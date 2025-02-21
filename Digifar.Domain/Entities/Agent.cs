using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class Agent
    {
        [Key]
        public Guid AgentId { get; set; } = Guid.NewGuid();

        [Required]
        public string? UserId { get; set; } // Foreign key to User

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string? AgentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NationalId { get; set; }

        public Guid? BusinessId { get; set; } // Nullable in case an agent is not linked to a business

        [ForeignKey(nameof(BusinessId))]
        public virtual Business? Business { get; set; }

        public KycStatus KycStatus { get; set; } = KycStatus.Pending;

        public string? DocumentUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
