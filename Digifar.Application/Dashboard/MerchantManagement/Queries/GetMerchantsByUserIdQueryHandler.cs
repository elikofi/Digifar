using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Queries
{
    public class GetMerchantsByUserIdQueryHandler : IRequestHandler<GetMerchantsByUserIdQuery, Result<List<Merchant>>>
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetMerchantsByUserIdQueryHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Result<List<Merchant>>> Handle(GetMerchantsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _merchantRepository.GetMerchantsAsync(request.UserId);
        }
    }
}