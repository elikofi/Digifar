using FluentValidation;

namespace Digifar.Application.Authentication.UserManagement.Commands.OTP
{
    public class RequestOtpCommandValidator :AbstractValidator<RequestOtpCommand>
    {
        public RequestOtpCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required.")
                .MinimumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .MaximumLength(10).WithMessage("Phonenumber has to be of 10 characters.")
                .Matches(@"^(\+233|0)[235][02456789]\d{7}$");
        }
    }
}
