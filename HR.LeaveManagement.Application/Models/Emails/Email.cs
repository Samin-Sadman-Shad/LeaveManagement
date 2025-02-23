using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Models.Emails
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public List<string> CCs { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
