using FluentValidation;
using IssueManagement.Application.Accounts.Requests;

namespace IssueManagement.Infrastructure.Validations.Users
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(user => user.NewPassword)
                .NotEmpty()
                .WithMessage("New Password is required.")
                .Length(8, 16)
                .WithMessage("New password must be between 8 and 16 characters.");
        }
    }
}
