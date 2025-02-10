
namespace Digifar.Application.Common.Interfaces.Services
{
    public interface IEmailVerificationService
    {
        Task<bool> SendVerificationEmailAsync(string email);
        Task<bool> VerifyEmailAsync(string email, string code);
        Task<bool> IsEmailVerified(string email);
    }
}
