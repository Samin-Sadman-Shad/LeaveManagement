using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public class LeaveAllocationDto : BaseQueryDto, ILeaveAllocationDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
