namespace IssueManagement.Application.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string message = "Email already exists.") : base(message)
        {
        }
    }
}
