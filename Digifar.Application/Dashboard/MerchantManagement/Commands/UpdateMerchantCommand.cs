using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public record UpdateMerchantCommand
    (
        Guid MerchantId,
        string? TaxIdentificationNumber,
        string? BusinessRegistrationNo,
        string? MerchantAddress,
        string? DocumentUrl
    ) : IRequest<Result<string>>;
}