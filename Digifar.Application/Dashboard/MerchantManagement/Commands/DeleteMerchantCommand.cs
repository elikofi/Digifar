using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public record DeleteMerchantCommand(Guid MerchantId) : IRequest<Result<string>>;
}