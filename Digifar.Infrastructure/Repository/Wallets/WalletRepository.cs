using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Application.Dashboard.WalletManagement.Models;
using Digifar.Domain.Entities;
using Digifar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Digifar.Infrastructure.Repository.Wallets
{
    public class WalletRepository(DigifarDbContext context) : IWalletRepository
    {
        public async Task<Result<string>> AddWalletAsync(Wallet wallet)
        {
            context.Wallets.Add(wallet);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("Wallet added successfully.");
        }

        public async Task<Result<string>> DeleteWalletAsync(Guid walletId)
        {
            var wallet = await context.Wallets.FindAsync(walletId);
            if (wallet is null)
                return Result<string>.ErrorResult("No wallet with this ID exists.");

            context.Wallets.Remove(wallet);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("Deleted successfully.");
        }

        public async Task<Result<Wallet?>> GetWalletByIdAsync(Guid walletId)
        {
            var wallet = await context.Wallets.FindAsync(walletId);

            if(wallet is null)
                return Result<Wallet?>.ErrorResult("Wallet with this wallet ID doesn't exist.");

            return Result<Wallet?>.SuccessResult(wallet);
        }

        public async Task<Result<List<WalletDTO>>> GetWalletsAsync(string userId)
        {
            var wallets = await context.Wallets
                                     .Where(w => w.UserId == userId)
                                     .AsNoTracking()
                                     .Select(w => new WalletDTO
                                     {
                                         WalletId = w.WalletId,
                                         UserId = w.UserId!,
                                         FirstName = w.User!.FirstName,
                                         LastName = w.User!.LastName,
                                         Balance = w.Balance,
                                         Currency = w.Currency,
                                         WalletType = w.WalletType,
                                         IsLocked = w.IsLocked,
                                         CreatedAt = w.CreatedAt,
                                         UpdatedAt = w.UpdatedAt
                                     })
                                     .ToListAsync();

            if (wallets.Count == 0)
                return Result<List<WalletDTO>>.ErrorResult("No wallets found for this user.");

            return Result<List<WalletDTO>>.SuccessResult(wallets);
        }

        public Task<Result<string>> UpdateWalletAsync(Wallet wallet)
        {
            throw new NotImplementedException();
        }
    }
}
