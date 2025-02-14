using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands
{
    public class UpdateLeaveTypeCommand:IRequest<BaseCommandResponse>
    {
        public UpdateLeaveTypeDto leaveTypeDto { get; set; } = null!;
    }
}
