
namespace IssueManagement.Application.Exceptions
{
    public class ConfirmationException : Exception
    {
        public ConfirmationException(string? message = "Ivalid otp") : base(message)
        {
        }
    }
}
