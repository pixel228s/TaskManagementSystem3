namespace IssueManagement.Application.Accounts.Requests
{
    public class ChangePasswordRequest
    {
        public required string Email { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
