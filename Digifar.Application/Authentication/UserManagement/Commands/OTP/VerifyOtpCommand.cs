using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public record VerifyOtpCommand(string PhoneNumber, string Otp): IRequest<Result<bool>>;
}
