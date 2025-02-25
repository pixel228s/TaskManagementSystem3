namespace IssueManagement.Application.Issues.requests
{
    public class CreateIssueRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public int PriorityId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
