namespace IssueManagement.Application.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string? message = "Authentication failed") : base(message)
        {
        }
    }
}
