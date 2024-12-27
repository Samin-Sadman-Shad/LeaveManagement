﻿using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO
{
    public class LeaveTypeDto:BaseDto
    {
        public LeaveTypeEnum LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}