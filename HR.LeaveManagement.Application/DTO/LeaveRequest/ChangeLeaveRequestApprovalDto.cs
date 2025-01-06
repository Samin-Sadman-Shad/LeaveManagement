using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public class ChangeLeaveRequestApprovalDto:BaseDto
    {
        public bool? IsApproved { get; set; }
        public int? ActionTakenById { get; set; }
        public DateTime? DateActioned { get; set; }
    }
}
