using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagement.Application.Users.Requests
{
    public class UserUpdateModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
