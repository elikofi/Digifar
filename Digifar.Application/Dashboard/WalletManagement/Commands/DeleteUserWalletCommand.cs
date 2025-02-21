using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public record DeleteUserWalletCommand(Guid WalletId) : IRequest<Result<string>>;
}
