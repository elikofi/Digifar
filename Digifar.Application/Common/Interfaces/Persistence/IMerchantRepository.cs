using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IMerchantRepository
    {
        Task<Result<string>> AddMerchantAsync(Merchant merchant);
        Task<Result<string>> DeleteMerchantAsync(Guid merchantId);
        Task<Result<Merchant?>> GetMerchantByIdAsync(Guid merchantId);
        Task<Result<List<Merchant>>> GetMerchantsAsync(string userId);
        Task<Result<string>> UpdateMerchantAsync(Merchant merchant);
    }
}