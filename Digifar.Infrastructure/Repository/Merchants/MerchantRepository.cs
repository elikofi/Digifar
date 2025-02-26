using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using Digifar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Digifar.Infrastructure.Repository.Merchants
{
    public class MerchantRepository(DigifarDbContext context): IMerchantRepository
    {
        public async Task<Result<string>> AddMerchantAsync(Merchant merchant)
        {
            context.Merchants.Add(merchant);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("Merchant added successfully.");
        }

        public async Task<Result<string>> DeleteMerchantAsync(Guid merchantId)
        {
            var merchant = await context.Merchants.FindAsync(merchantId);
            if (merchant is null)
                return Result<string>.ErrorResult("No merchant with this ID exists.");

            context.Merchants.Remove(merchant);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("Merchant deleted successfully.");
        }

        public async Task<Result<Merchant?>> GetMerchantByIdAsync(Guid merchantId)
        {
            var merchant = await context.Merchants.FindAsync(merchantId);

            if (merchant is null)
                return Result<Merchant?>.ErrorResult("Merchant with this ID doesn't exist.");

            return Result<Merchant?>.SuccessResult(merchant);
        }

        public async Task<Result<List<Merchant>>> GetMerchantsAsync(string userId)
        {
            var merchants = await context.Merchants
                                         .Where(m => m.UserId == userId)
                                         .AsNoTracking()
                                         .ToListAsync();

            if (merchants.Count == 0)
                return Result<List<Merchant>>.ErrorResult("No merchants found for this user.");

            return Result<List<Merchant>>.SuccessResult(merchants);
        }

        public async Task<Result<string>> UpdateMerchantAsync(Merchant merchant)
        {
            var existingMerchant = await context.Merchants.FindAsync(merchant.MerchantId);
            if (existingMerchant is null)
                return Result<string>.ErrorResult("No merchant with this ID exists.");

            context.Entry(existingMerchant).CurrentValues.SetValues(merchant);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("Merchant updated successfully.");
        }
    }
}