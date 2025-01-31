using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public class LeaveRequestDto : BaseQueryDto, ILeaveRequestDto
    {
        public int LeaveTypeId { get; set; }
        //public LeaveTypeDto LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public string? RequestComment { get; set; }

        public bool Cancelled { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? DateActioned { get; set; }
        //public string? ActionTakenBy { get; set; }
        public int? ActionTakenById { get; set; }
    }
}
