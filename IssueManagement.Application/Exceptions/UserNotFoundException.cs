﻿namespace IssueManagement.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message = "User not found") : base(message)
        {
        }
    }
}
