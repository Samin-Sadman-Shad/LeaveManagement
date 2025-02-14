﻿using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses.LeaveType
{
    public class CreateLeaveTypeDtoCommandResponse:CreateCommandResponse
    {
        public CreateLeaveTypeDto Record {  get; set; }
    }
}
