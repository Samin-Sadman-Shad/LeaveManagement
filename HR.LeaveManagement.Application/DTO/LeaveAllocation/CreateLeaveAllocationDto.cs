using HR.LeaveManagement.Application.DTO.LeaveType;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public class CreateLeaveAllocationDto: ILeaveAllocationDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public int Period { get; set; }

        public string EmployeeId { get; set; }

        public string CreatedById { get; set; }
    }
}
