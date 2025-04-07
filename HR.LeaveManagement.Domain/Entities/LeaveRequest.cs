using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Entities
{
    //no need to save Employee data directly in Entity
    //relationship with no navigation
    //relationship not discovered by convention as there is no reference navigation property
    //but employee is not an Entity
    public class LeaveRequest:BaseDomainEntity
    {
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } 
        public DateTime StartDate{ get; set; } = DateTime.Now;
        public DateTime EndDate{ get; set; }

        public string? RequestComment { get; set; }
        public bool? Approved { get; set; }
        public bool Canceled { get; set; }
        public DateTime? DateActioned { get; set; }
        /*        public Employee? ActionTakenBy { get; set; }*/
        public string? ActionTakenById { get; set; }

        //only foreign key property is required on the Dependent entity
        //non nullable, required relationship
        //reference navigation property to principal is optional
        public string EmployeeId { get; set; }
    }
}
