using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public class LeaveRequestListDto : BaseQueryDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }

        public DateTime DateRequested { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
