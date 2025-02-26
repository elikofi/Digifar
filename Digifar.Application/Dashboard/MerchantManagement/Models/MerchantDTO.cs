namespace Digifar.Application.Dashboard.MerchantManagement.Models
{
    public class MerchantDTO
    {
        public Guid MerchantId { get; set; }
        public string? UserId { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? BusinessRegistrationNo { get; set; }
        public string? MerchantAddress { get; set; }
        public string? DocumentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}