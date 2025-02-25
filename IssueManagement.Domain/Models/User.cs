using Microsoft.AspNetCore.Identity;

namespace IssueManagement.Domain.Models
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Issue>? Issues { get; set; }
    }
}
