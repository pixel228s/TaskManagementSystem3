﻿namespace IssueManagement.Application.Accounts.Requests
{
    public class RegisterRequestModel
    {
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
