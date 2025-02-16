using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagement.Application.Users.Requests
{
    public class ChangePasswordRequest
    {
        public required string Email { get; set; }  
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
