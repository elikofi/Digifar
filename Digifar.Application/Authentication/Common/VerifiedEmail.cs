namespace Digifar.Application.Authentication.Common
{
    public class VerifiedEmail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public DateTime VerifiedAt { get; set; }
    }
}
