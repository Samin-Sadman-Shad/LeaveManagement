using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO
{
    public class LeaveAllocationDto:BaseDto
    {
        public LeaveType LeaveType { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
    }
}
