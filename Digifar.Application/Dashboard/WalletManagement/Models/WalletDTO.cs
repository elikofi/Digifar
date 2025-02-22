using Digifar.Domain.Entities;

namespace Digifar.Application.Dashboard.WalletManagement.Models
{
    public class WalletDTO
    {
        public Guid WalletId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public CurrencyType Currency { get; set; }
        public WalletType WalletType { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
