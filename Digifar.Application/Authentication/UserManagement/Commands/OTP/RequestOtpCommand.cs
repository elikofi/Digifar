﻿using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public record RequestOtpCommand(string PhoneNumber): IRequest<Result<string>>;
}
