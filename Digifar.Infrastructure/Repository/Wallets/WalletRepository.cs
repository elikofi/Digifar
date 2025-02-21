using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
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

        public Task<Result<string>> DeleteWalletAsync(int walletId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Wallet?>> GetWalletByIdAsync(int walletId)
        {
            var wallet = await context.Wallets.FindAsync(walletId);

            if(wallet is null)
                return Result<Wallet?>.ErrorResult("Wallet with this wallet ID doesn't exist.");

            return Result<Wallet?>.SuccessResult(wallet);
        }

        public async Task<List<Wallet>> GetWalletsAsync(string userId)
        {
            return await context.Wallets.Where(w => w.UserId == userId).ToListAsync();
        }

        public Task<Result<string>> UpdateWalletAsync(Wallet wallet)
        {
            throw new NotImplementedException();
        }
    }
}
