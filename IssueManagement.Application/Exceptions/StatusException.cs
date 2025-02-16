namespace IssueManagement.Application.Exceptions
{
    public class StatusException : Exception
    {
        public StatusException(string? message = "This status can not be set") : base(message)
        {
        }
    }
}
