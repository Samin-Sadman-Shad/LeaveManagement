﻿using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.Responses.LeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands
{
    public class CreateLeaveTypeCommand:IRequest<CreateLeaveTypeDtoCommandResponse>

    {
        public CreateLeaveTypeDto leaveTypeDto { get; set; } = null!;
    }
}
