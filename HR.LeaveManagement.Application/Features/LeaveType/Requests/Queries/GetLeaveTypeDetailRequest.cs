﻿using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveType.Requests.Queries
{
    public class GetLeaveTypeDetailRequest : IRequest<BaseQueryResponse<LeaveTypeDto>>
    {
        public int Id { get; set; }
    }
}
