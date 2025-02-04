using Digifar.Application.Common.Results;

namespace Digifar.Application.Common.Interfaces.Authentication
{
    public interface IOtpService
    {
        Task<Result<string>> RequestOTP(string phoneNumber);
        Task<Result<bool>> VerifyOTP(string phoneNumber, string otp);
    }
}
