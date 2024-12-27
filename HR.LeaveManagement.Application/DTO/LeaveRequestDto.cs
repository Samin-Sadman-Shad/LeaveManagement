using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO
{
    public class LeaveRequestDto:BaseDto
    {
        public LeaveType LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string? RequestComment { get; set; }
        public bool? Approved { get; set; }
        public bool Canceled { get; set; }
        public DateTime? DateActioned { get; set; }
        public string? ActionTakenBy { get; set; }
        public int? AxtionTakenById { get; set; }
    }
}
