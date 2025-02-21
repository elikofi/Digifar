using FluentValidation;

namespace Digifar.Application.Dashboard.WalletManagement.Queries
{
    public class GetAllWalletsOfAUserQueryValidator : AbstractValidator<GetAllWalletsOfAUserQuery>
    {
        public GetAllWalletsOfAUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
