namespace IssueManagement.Application.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException(string? message = "This username is already taken.") : base(message)
        {
        }
    }
}
