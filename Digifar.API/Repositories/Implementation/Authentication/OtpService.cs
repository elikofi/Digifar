using Digifar.API.Data;
using Digifar.API.Models.DTOs;
using Digifar.API.Repositories.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace Digifar.API.Repositories.Implementation.Authentication
{

    public class OtpService : IOtpService
    {
        private readonly DigifarDbContext _context;

        public OtpService(DigifarDbContext context)
        {
            _context = context;
        }

        private const int OtpExpirationMinutes = 5;


        public static string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            int number = (int)(BitConverter.ToUInt32(bytes, 0) % 1000000);
            return number.ToString("D6");
        }


        public async Task<string> RequestOTP(string phoneNumber)
        {

            var numberExists = await _context.Otps.AnyAsync(o => o.PhoneNumber == phoneNumber);
            if (!numberExists) return "number does not exist.";

            var otp = GenerateOtp();

            var otpRecord = new OtpRecord
            {
                PhoneNumber = phoneNumber,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(OtpExpirationMinutes)
            };
            _context.Otps.Add(otpRecord);

            await _context.SaveChangesAsync();

            return otp;
        }

        public async Task<bool> VerifyOTP(string phoneNumber, string otp)
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
                return false;
            }

            if (otp == otpRecord.Otp)
            {
                _context.Otps.Remove(otpRecord);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
