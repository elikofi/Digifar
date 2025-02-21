using Digifar.Domain.Entities;

namespace Digifar.Contracts.Wallets
{
    public record CreateUserWalletRequest
        (
            string UserId,
            CurrencyType Currency,
            WalletType WalletType
        );
}
