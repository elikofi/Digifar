using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public class VerifyOtpCommandHandler(IOtpService otpService) : IRequestHandler<VerifyOtpCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            return await otpService.VerifyOTP(request.PhoneNumber, request.Otp);

        }
    }
}
