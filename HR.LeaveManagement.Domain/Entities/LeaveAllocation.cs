using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Entities
{
    public class LeaveAllocation:BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
    }
}
