using Microsoft.AspNetCore.Identity;

namespace Digifar.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double Wallet { get; set; } = 0;
        public bool IsOtpVerified { get; set; }
    }
}
