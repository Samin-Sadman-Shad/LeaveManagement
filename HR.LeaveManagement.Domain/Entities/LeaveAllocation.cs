using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Entities
{
    //no need to save Employee data directly in Entity
    public class LeaveAllocation:BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } 
        public int NumberOfDays { get; set; }
        public int Period { get; set; }

        //only foreign key property is required on the Dependent entity
        //non nullable, required relationship
        //reference navigation property to principal is optional
        public string EmployeeId { get; set; }
    }
}
