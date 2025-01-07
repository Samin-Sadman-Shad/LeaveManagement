using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    public interface ILeaveTypeDto
    {
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
