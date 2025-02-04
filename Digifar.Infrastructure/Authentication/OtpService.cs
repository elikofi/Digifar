using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Results;
using Digifar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Digifar.Infrastructure.Authentication
{

    public class OtpService : IOtpService
    {
        private readonly DigifarDbContext _context;

        public OtpService(DigifarDbContext context)
        {
            _context = context;
        }
        //I'll move this to the appsettings later.
        private const int OtpExpirationMinutes = 5;



        public static string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            int number = (int)(BitConverter.ToUInt32(bytes, 0) % 1000000);
            return number.ToString("D6");
        }


        public async Task<Result<string>> RequestOTP(string phoneNumber)
        {


            var otp = GenerateOtp();

            var otpRecord = new OtpRecord
            {
                PhoneNumber = phoneNumber,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(OtpExpirationMinutes)
            };
            _context.Otps.Add(otpRecord);

            await _context.SaveChangesAsync();

            return Result<string>.SuccessResult(otp);
        }

        public async Task<Result<bool>> VerifyOTP(string phoneNumber, string otp)
        {
            var otpRecord = await _context.Otps
                .FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber);

            if (otpRecord == null || DateTime.UtcNow > otpRecord.ExpiryTime)
            {
                if (otpRecord != null)
                {
                    _context.Otps.Remove(otpRecord);
                    await _context.SaveChangesAsync();
                }
                return Result<bool>.ErrorResult("Error occured, unable to verify OTP.");
            }

            if (otp == otpRecord.Otp)
            {
                _context.Otps.Remove(otpRecord);
                await _context.SaveChangesAsync();
                return Result<bool>.SuccessResult(true);
            }

            return Result<bool>.ErrorResult("Error occured, phonenumber doesn't exist.");
        }
    }
}
