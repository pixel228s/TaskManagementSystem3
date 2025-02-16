using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagement.Domain.Models
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Issue>? Issues { get; set; }
    }
}
