using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Application.Dashboard.WalletManagement.Models;
using MediatR;

namespace Digifar.Application.Dashboard.WalletManagement.Queries
{
    public class GetAllWalletsOfAUserQueryHandler(IWalletRepository walletRepository) : IRequestHandler<GetAllWalletsOfAUserQuery, Result<List<WalletDTO>>>
    {
        public async Task<Result<List<WalletDTO>>> Handle(GetAllWalletsOfAUserQuery request, CancellationToken cancellationToken)
        {
            var wallet = await walletRepository.GetWalletsAsync(request.UserId);

            return wallet!;
        }
    }
}
