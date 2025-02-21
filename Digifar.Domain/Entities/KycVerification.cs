using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class KycVerification
    {
        [Key]
        public Guid KycId { get; set; } = Guid.NewGuid();

        public string? UserId { get; set; } // Foreign key to User

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public KycStatus Status { get; set; } = KycStatus.Pending;

        public string? DocumentUrl { get; set; }
        public string? ReasonForRejection { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }

    public enum KycStatus
    {
        Approved = 1,
        Pending = 2,
        Rejected = 0
    }
}
