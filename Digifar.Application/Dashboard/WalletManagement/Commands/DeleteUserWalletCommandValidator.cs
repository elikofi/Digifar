using FluentValidation;

namespace Digifar.Application.Dashboard.WalletManagement.Commands
{
    public class DeleteUserWalletCommandValidator : AbstractValidator<DeleteUserWalletCommand>
    {
        public DeleteUserWalletCommandValidator()
        {
            RuleFor(x => x.WalletId).NotEmpty().WithMessage("Wallet ID is required.");
        }
    }
}
