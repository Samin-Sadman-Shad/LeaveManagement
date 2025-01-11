using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    /// <summary>
    /// Fields those are common for every create and update Dto, as they needed to be validated
    /// </summary>
    public interface ILeaveRequestDto:IBaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? RequestComment { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
