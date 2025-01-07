using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Responses
{
    public class BaseCommandResponse: BaseResponse
    {
        public int? RecordId { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
