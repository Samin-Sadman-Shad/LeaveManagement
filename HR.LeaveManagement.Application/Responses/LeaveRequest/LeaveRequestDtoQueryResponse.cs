﻿using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses.LeaveRequest
{
    public class LeaveRequestDtoQueryResponse:BaseQueryResponse
    {
        public LeaveRequestDto Record { get; set; }
    }
}
