using FluentValidation;

namespace Digifar.Application.Authentication.UserManagement.Commands.Register
{
    public class RegisterUserCommandValidator :AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required.")
                .MinimumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .MaximumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .Matches(@"^(\+233|0)[235][023456789]\d{7}$");

        }
    }
}
