using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HR.LeaveManagement.Application.Responses.Common
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
    }
}
