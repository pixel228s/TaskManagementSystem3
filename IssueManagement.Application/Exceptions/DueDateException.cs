namespace IssueManagement.Application.Exceptions
{
    public class DueDateException : Exception
    {
        public DueDateException(string? message = "Due date can not be in the past") : base(message)
        {
        }
    }
}
