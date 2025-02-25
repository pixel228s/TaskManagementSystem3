namespace IssueManagement.Application.Users.Requests
{
    public class UserPostRequestModel
    {
        public string UserName {  get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
