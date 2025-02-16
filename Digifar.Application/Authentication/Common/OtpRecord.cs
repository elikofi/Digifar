﻿namespace Digifar.Application.Authentication.Common
{
    public class OtpRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? PhoneNumber { get; set; }
        public string? Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
