
using FluentValidation;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public class CreateUserWalletCommandValidator : AbstractValidator<CreateUserWalletCommand>
    {
        public CreateUserWalletCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency type is required.");
            RuleFor(x => x.WalletType).NotEmpty().WithMessage("Wallet type is required.");

        }
    }
}
