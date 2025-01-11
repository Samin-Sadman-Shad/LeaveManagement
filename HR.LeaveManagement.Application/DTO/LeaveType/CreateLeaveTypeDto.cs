using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    public class CreateLeaveTypeDto:ILeaveTypeDto
    {
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
