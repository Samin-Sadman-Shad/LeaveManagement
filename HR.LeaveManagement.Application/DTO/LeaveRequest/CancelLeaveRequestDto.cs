using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public class CancelLeaveRequestDto:BaseQueryDto
    {
        public bool Cancel {  get; set; }
    }
}
