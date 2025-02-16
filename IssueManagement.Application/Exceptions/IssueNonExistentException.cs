namespace IssueManagement.Application.Exceptions
{
    public class IssueNonExistentException : Exception
    {
        public IssueNonExistentException(string? message = "Issue does not exist") : base(message)
        {
        }
    }
}
