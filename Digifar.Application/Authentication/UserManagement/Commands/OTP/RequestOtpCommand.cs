using Digifar.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public record RequestOtpCommand(string PhoneNumber): IRequest<Result<string>>;
}
