using Digifar.Application.Common.Interfaces.Persistence;
using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public class DeleteMerchantCommandHandler(IMerchantRepository merchantRepository)
        : IRequestHandler<DeleteMerchantCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
        {
            return await merchantRepository.DeleteMerchantAsync(request.MerchantId);
        }
    }
}