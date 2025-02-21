using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IWalletRepository
    {
        Task<List<Wallet>> GetWalletsAsync(string userId);
        Task<Result<Wallet?>> GetWalletByIdAsync(int walletId);
        Task<Result<string>> AddWalletAsync(Wallet wallet);
        Task<Result<string>>UpdateWalletAsync(Wallet wallet);
        Task<Result<string>> DeleteWalletAsync(int walletId);
    }
}
