using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    internal class CreateLeaveRequestDto 
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? RequestComment { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
