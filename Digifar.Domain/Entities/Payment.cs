
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        [Required]
        public string? UserId { get; set; } // Foreign key to User

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(10)]
        public CurrencyType? Currency { get; set; } = CurrencyType.CFA;

        [Required]
        public PaymentMethod Method { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ReferenceId { get; set; } // External payment reference

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string? PaymentGatewayResponse { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum PaymentMethod
    {
        Card = 1,
        BankTransfer = 2,
        MobileMoney = 3,
        Cash = 4
    }

    public enum PaymentStatus
    {
        Successful = 1,
        Pending = 2,
        Failed = 0
    }
}
