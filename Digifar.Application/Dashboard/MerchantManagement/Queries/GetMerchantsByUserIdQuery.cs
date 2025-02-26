using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Queries
{
    public record GetMerchantsByUserIdQuery(string UserId) : IRequest<Result<List<Merchant>>>;
}