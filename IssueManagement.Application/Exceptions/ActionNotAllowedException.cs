using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagement.Application.Exceptions
{
    public class ActionNotAllowedException : Exception
    {
        public ActionNotAllowedException(string? message = "Action not authorized") : base(message)
        {
        }
    }
}
