using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, Result<string>>
    {
        private readonly IMerchantRepository _merchantRepository;

        public CreateMerchantCommandHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Result<string>> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = new Merchant
            {
                UserId = request.UserId,
                TaxIdentificationNumber = request.TaxIdentificationNumber,
                BusinessRegistrationNo = request.BusinessRegistrationNo,
                MerchantAddress = request.MerchantAddress,
                DocumentUrl = request.DocumentUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _merchantRepository.AddMerchantAsync(merchant);
        }
    }
}