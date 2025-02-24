using IssueManagement.Domain.Enums;
namespace IssueManagement.Application.Issues.requests
{
    public class IssuePutRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public PriorityTypes? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusTypes? Status { get; set; }
    }
}
