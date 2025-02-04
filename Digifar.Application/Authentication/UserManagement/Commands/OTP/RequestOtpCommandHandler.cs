using Digifar.Application.Authentication.Common;
using Digifar.Application.Common.Interfaces.Authentication;
using Digifar.Application.Common.Results;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
