using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Queries
{
    public record GetAllWalletsOfAUserQuery(string UserId) : IRequest<Result<List<Wallet>>>;
}
