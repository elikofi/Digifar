using Digifar.Application.Common.Results;
using Digifar.Domain.Entities;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Queries
{
    public record GetAllMerchantsQuery : IRequest<Result<List<Merchant>>>;
}