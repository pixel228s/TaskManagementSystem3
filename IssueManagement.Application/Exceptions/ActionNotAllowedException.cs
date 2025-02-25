namespace IssueManagement.Application.Exceptions
{
    public class ActionNotAllowedException : Exception
    {
        public ActionNotAllowedException(string? message = "Action not authorized") : base(message)
        {
        }
    }
}
