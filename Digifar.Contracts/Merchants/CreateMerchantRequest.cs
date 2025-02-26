namespace Digifar.Contracts.Merchants
{
    public record CreateMerchantRequest
    (
        string UserId,
        string? TaxIdentificationNumber,
        string? BusinessRegistrationNo,
        string? MerchantAddress,
        string? DocumentUrl
    );
}