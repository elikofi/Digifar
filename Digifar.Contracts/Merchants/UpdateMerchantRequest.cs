namespace Digifar.Contracts.Merchants
{
    public record UpdateMerchantRequest
    (
        Guid MerchantId,
        string? TaxIdentificationNumber,
        string? BusinessRegistrationNo,
        string? MerchantAddress,
        string? DocumentUrl
    );
}