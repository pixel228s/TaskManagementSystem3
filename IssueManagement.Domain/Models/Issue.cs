namespace IssueManagement.Domain.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public User? AssignedUser { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? PriorityID { get; set; }
        public Priority? Priority { get; set; }
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
