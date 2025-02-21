using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Services;
using Digifar.Infrastructure.Data;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Security.Cryptography;

namespace Digifar.Infrastructure.Services
{
    public class EmailVerificationService(IOptions<EmailSettings> emailOptions, 
        DigifarDbContext context,
        ILogger<EmailVerificationService> logger) : IEmailVerificationService
    {
        private readonly EmailSettings emailSettings = emailOptions.Value;


        public async Task<bool> IsEmailVerified(string email)
        {
            return await context.VerifiedEmails
            .AnyAsync(v => v.Email == email);
        }

        public async Task<bool> SendVerificationEmailAsync(string email)
        {
            try
            {
                var verificationCode = GenerateVerificationCode();
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Email Verification", emailSettings.Username));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Verify Your Email Address";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <h2 style='color: #333;'>Email Verification</h2>
                            <p>Your verification code is:</p>
                            <div style='background-color: #f5f5f5; padding: 10px; text-align: center; font-size: 24px; font-weight: bold; margin: 20px 0;'>
                                {verificationCode}
                            </div>
                            <p>This code will expire in {emailSettings.CodeExpirationMinutes} minutes.</p>
                            <p style='color: #666; font-size: 12px;'>If you didn't request this verification, please ignore this email.</p>
                        </div>
                    </body>
                    </html>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailSettings.Username, emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                await StoreVerificationCode(email, verificationCode);

                logger.LogInformation("Verification email sent successfully to {Email}", email);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending verification email to {Email}", email);
                return false;
            }
        }

        public async Task<bool> VerifyEmailAsync(string email, string code)
        {
            try
            {
                var alreadyVerified = await context.VerifiedEmails
                    .AnyAsync(v => v.Email == email);

                if (alreadyVerified)
                {
                    logger.LogInformation("Email {Email} is already verified", email);
                    return true;
                }

                var isValid = await ValidateVerificationCode(email, code);
                if (!isValid)
                {
                    logger.LogWarning("Invalid or expired verification code for {Email}", email);
                    return false;
                }

                using var client = new ImapClient();
                await client.ConnectAsync(emailSettings.ImapServer, emailSettings.ImapPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailSettings.Username, emailSettings.Password);

                var verifiedEmail = new VerifiedEmail
                {
                    Email = email,
                    VerifiedAt = DateTime.UtcNow
                };

                context.VerifiedEmails.Add(verifiedEmail);
                await context.SaveChangesAsync();

                logger.LogInformation("Email {Email} verified successfully", email);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error verifying email {Email}", email);
                return false;
            }
        }


        protected static string GenerateVerificationCode() 
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            int number = (int)(BitConverter.ToUInt32(bytes, 0) % 1000000);
            return number.ToString("D6");
        }

        private async Task StoreVerificationCode(string email, string code)
        {
            var verificationCode = new EmailVerificationCode
            {
                Email = email,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(emailSettings.CodeExpirationMinutes),
                IsUsed = false
            };

            context.VerificationCodes.Add(verificationCode);
            await context.SaveChangesAsync();
        }

        private async Task<bool> ValidateVerificationCode(string email, string code)
        {
            var verificationCode = await context.VerificationCodes
                .Where(v => v.Email == email && v.Code == code && !v.IsUsed)
                .OrderByDescending(v => v.CreatedAt)
                .FirstOrDefaultAsync();

            if (verificationCode == null)
                return false;

            if (verificationCode.ExpiresAt < DateTime.UtcNow)
                return false;

            // Mark code as used
            verificationCode.IsUsed = true;
            await context.SaveChangesAsync();

            return true;
        }


    }
}
