using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Queries
{
    public class GetMerchantByIdQueryHandler : IRequestHandler<GetMerchantByIdQuery, Result<Merchant?>>
    {
        private readonly IMerchantRepository _merchantRepository;

        public GetMerchantByIdQueryHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<Result<Merchant?>> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
        {
            return await _merchantRepository.GetMerchantByIdAsync(request.MerchantId);
        }
    }
}