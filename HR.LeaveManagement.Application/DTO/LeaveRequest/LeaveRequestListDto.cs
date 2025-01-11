using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public class LeaveRequestListDto : BaseCommandDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }
    }
}
