using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using Digifar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Digifar.Infrastructure.Repository.KycVerification
{
    public class KycRepository(DigifarDbContext context) : IKycRepository
    {
        public async Task<Result<string>> AddKycAsync(Domain.Entities.KycVerification kyc)
        {
            context.KycVerifications.Add(kyc);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("KYC added successfully.");
        }

        public async Task<Result<string>> DeleteKycAsync(Guid kycId)
        {
            var kyc = await context.KycVerifications.FindAsync(kycId);
            if (kyc is null)
                return Result<string>.ErrorResult("No KYC with this ID exists.");

            context.KycVerifications.Remove(kyc);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("KYC deleted successfully.");
        }

        public async Task<Result<Domain.Entities.KycVerification?>> GetKycByIdAsync(Guid kycId)
        {
            var kyc = await context.KycVerifications.FindAsync(kycId);

            if (kyc is null)
                return Result<Domain.Entities.KycVerification?>.ErrorResult("KYC with this ID doesn't exist.");

            return Result<Domain.Entities.KycVerification?>.SuccessResult(kyc);
        }

        public async Task<Result<List<Domain.Entities.KycVerification>>> GetKycsByUserIdAsync(string userId)
        {
            var kycs = await context.KycVerifications
                                     .Where(k => k.UserId == userId)
                                     .AsNoTracking()
                                     .ToListAsync();

            if (kycs.Count == 0)
                return Result<List<Domain.Entities.KycVerification>>.ErrorResult("No KYC records found for this user.");

            return Result<List<Domain.Entities.KycVerification>>.SuccessResult(kycs);
        }

        public async Task<Result<string>> UpdateKycAsync(Domain.Entities.KycVerification kyc)
        {
            var existingKyc = await context.KycVerifications.FindAsync(kyc.KycId);
            if (existingKyc is null)
                return Result<string>.ErrorResult("No KYC with this ID exists.");

            context.Entry(existingKyc).CurrentValues.SetValues(kyc);
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("KYC updated successfully.");
        }

        public async Task<Result<string>> UpdateKycStatusAsync(Guid kycId, KycStatus status)
        {
            var kyc = await context.KycVerifications.FindAsync(kycId);
            if (kyc is null)
                return Result<string>.ErrorResult("No KYC with this ID exists.");

            kyc.Status = status;
            await context.SaveChangesAsync();

            return Result<string>.SuccessResult("KYC status updated successfully.");
        }
    }
}