﻿using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO
{
    internal class LeaveRequestListDto:BaseDto
    {
        public LeaveType LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }
    }
}