using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Responses
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
