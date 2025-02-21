using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class Business
    {
        [Key]
        public Guid BusinessId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string? BusinessName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? BusinessType { get; set; }

        [Required]
        [MaxLength(100)]
        public string? BusinessRegistrationNo { get; set; }

        [MaxLength(100)]
        public string? TaxIdentificationNumber { get; set; }

        [Required]
        public string? BusinessAddress { get; set; }

        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? BusinessEmail { get; set; }

        [Required]
        public KycStatus KycStatus { get; set; } = KycStatus.Pending;

        public string? DocumentUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public virtual List<Merchant> Merchants { get; set; } = new();
    }
}
