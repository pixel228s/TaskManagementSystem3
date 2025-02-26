using FluentValidation;
using IssueManagement.Application.Issues.requests;

namespace IssueManagement.Infrastructure.Validations.Issue
{
    public class IssueCreationValidator : AbstractValidator<CreateIssueRequest>
    {

        public IssueCreationValidator() 
        {

            RuleFor(d => d.DueDate)
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("Due date can not be set to the past.");

            RuleFor(d => d.Title)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("Title can't be null or empty.");

            RuleFor(d => d.Title)
                 .MaximumLength(100)
                 .WithMessage("Title can have maximum length of 100.");
        }
    }
}
