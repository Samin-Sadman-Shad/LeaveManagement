using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public class UpdateLeaveAllocationDto:BaseDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
    }
}
