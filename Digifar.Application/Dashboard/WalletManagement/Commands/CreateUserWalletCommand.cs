﻿
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public record CreateUserWalletCommand
        (
            string UserId,
            CurrencyType Currency,
            WalletType WalletType
        ) : IRequest<Result<string>>;
}
