using Digifar.Application.Common.Results;
using MediatR;

namespace Digifar.Application.Dashboard.MerchantManagement.Commands
{
    public record CreateMerchantCommand
    (
        string UserId,
        string? TaxIdentificationNumber,
        string? BusinessRegistrationNo,
        string? MerchantAddress,
        string? DocumentUrl
    ) : IRequest<Result<string>>;
}