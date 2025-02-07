using FluentValidation;

namespace Digifar.Application.Authentication.UserManagement.Queries.Login
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required.")
                .MinimumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .MaximumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .Matches(@"^(\+233|0)[235][023456789]\d{7}$");
            RuleFor(x => x.Otp).NotEmpty().WithMessage("OTP is required.")
                .MinimumLength(6).WithMessage("OTP has to be at least 6 characters.")
                .MaximumLength(6).WithMessage("OTP can not be more than 6 characters.");
        }
    }
}
