namespace IssueManagement.Application.Users.Requests
{
    public class ChangePasswordRequest
    {
        public required string Email { get; set; }  
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
