﻿using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Domain.Entities
{
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
        public string? RequestingEmployeeId { get; set; }
    }
}
