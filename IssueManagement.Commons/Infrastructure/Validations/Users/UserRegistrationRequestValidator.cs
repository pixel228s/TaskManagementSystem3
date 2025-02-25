using FluentValidation;
using IssueManagement.Application.Accounts.Requests;

namespace IssueManagement.Infrastructure.Validations.Users
{
    public class UserRegistrationRequestValidator : AbstractValidator<RegisterRequestModel>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Length(8, 16)
                .WithMessage("Password must be between 8 and 16 characters.");

            RuleFor(user => user.UserName)
                .NotEmpty();
        }
    }
}
