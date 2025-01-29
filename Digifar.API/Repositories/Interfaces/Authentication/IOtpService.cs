namespace Digifar.API.Repositories.Interfaces.Authentication
{
    public interface IOtpService
    {
        Task<string> RequestOTP(string phoneNumber);
        Task<bool> VerifyOTP(string phoneNumber, string otp);
    }
}
