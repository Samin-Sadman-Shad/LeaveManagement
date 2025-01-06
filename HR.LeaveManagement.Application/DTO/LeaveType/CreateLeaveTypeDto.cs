using HR.LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    internal class CreateLeaveTypeDto
    {
        public LeaveTypeEnum LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
