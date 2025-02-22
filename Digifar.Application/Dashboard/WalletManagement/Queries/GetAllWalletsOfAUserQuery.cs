using Digifar.Application.Common.Results;
using Digifar.Application.Dashboard.WalletManagement.Models;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Queries
{
    public record GetAllWalletsOfAUserQuery(string UserId) : IRequest<Result<List<WalletDTO>>>;
}
