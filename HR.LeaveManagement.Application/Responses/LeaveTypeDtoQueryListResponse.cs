﻿using HR.LeaveManagement.Application.DTO.LeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses
{
    public class LeaveTypeDtoQueryListResponse:BaseQueryListResponse
    {
        public List<LeaveTypeDto> Records {  get; set; }
    }
}
