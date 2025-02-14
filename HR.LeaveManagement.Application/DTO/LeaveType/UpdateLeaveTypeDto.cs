using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    /// <summary>
    /// Does not inherit from BaseQueryDto as the user is not allowed to modify Id
    /// </summary>
    public class UpdateLeaveTypeDto: ILeaveTypeDto
    {
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
