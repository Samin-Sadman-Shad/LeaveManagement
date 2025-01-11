using HR.LeaveManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public interface ILeaveAllocationDto:IBaseDto
    {
        public int LeaveTypeId { get; set; }
        public int NumberOfDays { get; set; }
        public int Period { get; set; }
    }
}
