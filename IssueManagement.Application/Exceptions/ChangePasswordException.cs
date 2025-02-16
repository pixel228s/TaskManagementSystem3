namespace IssueManagement.Application.Exceptions
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException(string? message = "Error occured while changing password.") : base(message)
        {
        }
    }
}
