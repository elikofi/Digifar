using FluentValidation;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public class VerifyOtpCommandValidator :AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                .MinimumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .MaximumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .Matches(@"^(\+233|0)[235][02456789]\d{7}$"); ;
            RuleFor(x => x.Otp).NotEmpty().WithMessage("OTP is required")
                .MinimumLength(6).WithMessage("One Time Token has to be of 6 characters.");
        }
    }
}
