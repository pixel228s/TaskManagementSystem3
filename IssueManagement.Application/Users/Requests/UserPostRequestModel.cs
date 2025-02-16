using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagement.Application.Users.Requests
{
    public class UserPostRequestModel
    {
        public string UserName {  get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
