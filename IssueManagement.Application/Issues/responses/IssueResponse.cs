using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;

namespace IssueManagement.Application.Issues.responses
{
    public class IssueResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Status Status { get; set; } = Status.ToDo;
    }
}
