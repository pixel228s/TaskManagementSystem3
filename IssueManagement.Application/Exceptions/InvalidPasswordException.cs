namespace IssueManagement.Application.Exceptions
{
    public class InvalidPasswordException : Exception 
    {
        public InvalidPasswordException(string message = "Invalid password") : base(message) 
        {
        }
    }
}
