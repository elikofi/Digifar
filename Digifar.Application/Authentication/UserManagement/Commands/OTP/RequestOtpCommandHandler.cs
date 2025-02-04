using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Results;
using MapsterMapper;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public class RequestOtpCommandHandler(IOtpService otpService, IMapper mapper) : IRequestHandler<RequestOtpCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RequestOtpCommand command, CancellationToken cancellationToken)
        {
            //var phoneNumber = mapper.Map<OtpRecord>(command);

            return await otpService.RequestOTP(command.PhoneNumber);

        }
    }
}
