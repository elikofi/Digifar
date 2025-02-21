using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IWalletRepository
    {
        Task<Result<List<Wallet?>>> GetWalletsAsync(string userId);
        Task<Result<Wallet?>> GetWalletByIdAsync(Guid walletId);
        Task<Result<string>> AddWalletAsync(Wallet wallet);
        Task<Result<string>>UpdateWalletAsync(Wallet wallet);
        Task<Result<string>> DeleteWalletAsync(Guid walletId);
    }
}
