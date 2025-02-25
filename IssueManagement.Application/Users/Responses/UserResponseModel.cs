using IssueManagement.Domain.Models;

namespace IssueManagement.Application.Users.Responses
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Issue>? Issues { get; set; }
    }
}
