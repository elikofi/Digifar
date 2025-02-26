using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;

namespace Digifar.Application.Common.Interfaces.Persistence
{
    public interface IKycRepository
    {
        Task<Result<string>> AddKycAsync(KycVerification kyc);
        Task<Result<string>> DeleteKycAsync(Guid kycId);
        Task<Result<KycVerification?>> GetKycByIdAsync(Guid kycId);
        Task<Result<List<KycVerification>>> GetKycsByUserIdAsync(string userId);
        Task<Result<string>> UpdateKycAsync(KycVerification kyc);
        Task<Result<string>> UpdateKycStatusAsync(Guid kycId, KycStatus status);
    }
}