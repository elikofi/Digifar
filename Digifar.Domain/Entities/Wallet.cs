using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Digifar.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; } = Guid.NewGuid();

        [Required]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Required]
        public decimal Balance { get; set; } = 0.00m;

        [Required]
        public CurrencyType Currency { get; set; } = CurrencyType.CFA;

        public WalletType WalletType { get; set; } = WalletType.Primary;

        public bool IsLocked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }

    public enum CurrencyType
    {   
        CFA = 1,
        GHS = 2
    }
    public enum WalletType
    { 
        Primary = 1, 
        Secondary = 2
    }

}
