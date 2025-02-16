using FluentValidation;
using IssueManagement.Application.Accounts.Requests;
namespace IssueManagement.Commons.Validators.Users
{
    public class RegistrationRequestValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(user => user)
        }
    }
}
