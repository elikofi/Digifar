using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class Merchant
    {
        [Key]
        public Guid MerchantId { get; set; } = Guid.NewGuid();

        [Required]
        public string? UserId { get; set; } // Foreign Key

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public virtual List<Business> Businesses { get; set; } = [];

        [MaxLength(100)]
        public string? TaxIdentificationNumber { get; set; }

        [MaxLength(100)]
        public string? BusinessRegistrationNo { get; set; }

        public string? MerchantAddress { get; set; }

        public KycStatus KycStatus { get; set; } = KycStatus.Pending;

        public bool IsVerified => KycStatus == KycStatus.Approved;

        public string? DocumentUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
