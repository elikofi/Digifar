using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Interfaces.Services;
using Digifar.Application.Common.Results;
using Digifar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Digifar.Infrastructure.Authentication
{

    public class OtpService(DigifarDbContext _context, IMNotifySmsService smsService) : IOtpService
    {

        //I'll move this to the appsettings later.
        private const int OtpExpirationMinutes = 5;

        #region Twilio
        //Twilio info... to be moved to a secure place.
        //private const string accountSid = "AC2b602d5660a423d419c9f47267bfbb4a";
        //private const string authToken = "90c78dfdc93c7b1f4e53bada0346aa86";

        //public OtpService(DigifarDbContext context)
        //{
        //    _context = context;
        //    TwilioClient.Init(accountSid, authToken);
        //}
        ////twilio
        //string fromPhoneNumber = "+1 812 993 6028";
        //string toPhoneNumber = phoneNumber;

        //var message = MessageResource.Create(
        //    body: $"Your six digits OTP is: {otp}. Do not share.",
        //    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
        //    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
        //);
        #endregion


        protected static string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            int number = (int)(BitConverter.ToUInt32(bytes, 0) % 1000000);
            return number.ToString("D6");
        }

        public async Task<Result<string>> RequestOTP(string phoneNumber)
        {
            var existingOtp = await _context.Otps.FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber);

            if (existingOtp != null)
            {
                _context.Otps.Remove(existingOtp);
                await _context.SaveChangesAsync();
            }

            var otp = GenerateOtp();

            var otpRecord = new OtpRecord
            {
                PhoneNumber = phoneNumber,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(OtpExpirationMinutes)
            };
            _context.Otps.Add(otpRecord);

            await _context.SaveChangesAsync();

            await smsService.SendSmsAsync(phoneNumber, $"Your six digits OTP is: {otp}. Do not share.");


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
                return Result<bool>.ErrorResult("Otp expired, kindly request a new one.");
            }

            if (otp == otpRecord.Otp)
            {
                _context.Otps.Remove(otpRecord);
                await _context.SaveChangesAsync();
                return Result<bool>.SuccessResult(true);
            }

            return Result<bool>.ErrorResult("Phonenumber doesn't exist.");
        }
    }
}
