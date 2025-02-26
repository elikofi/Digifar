using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public class UpdateMerchantCommandHandler(IMerchantRepository merchantRepository)
        : IRequestHandler<UpdateMerchantCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = new Merchant
            {
                MerchantId = request.MerchantId,
                TaxIdentificationNumber = request.TaxIdentificationNumber,
                BusinessRegistrationNo = request.BusinessRegistrationNo,
                MerchantAddress = request.MerchantAddress,
                DocumentUrl = request.DocumentUrl,
                UpdatedAt = DateTime.UtcNow
            };

            return await merchantRepository.UpdateMerchantAsync(merchant);
        }
    }
}