namespace IssueManagement.Application.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string? message = "Could not register") : base(message)
        {
        }
    }
}
