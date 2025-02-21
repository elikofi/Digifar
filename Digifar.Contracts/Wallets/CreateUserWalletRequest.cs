using Digifar.Domain.Entities;

namespace Digifar.Contracts.Wallets
{
    public record CreateUserWalletRequest
        (
            string UserId,
            decimal Balance,
            CurrencyType Currency,
            WalletType WalletType
        );
}
