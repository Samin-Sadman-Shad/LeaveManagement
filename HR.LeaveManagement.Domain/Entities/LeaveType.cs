using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Entities
{
    public class LeaveType:BaseDomainEntity
    {
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
