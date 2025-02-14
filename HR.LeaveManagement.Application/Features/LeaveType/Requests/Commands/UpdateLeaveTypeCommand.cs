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
        //Id will be set by the route parameter from request 
        public int Id { get; set; }
        //user might not allow to change the Id
        public UpdateLeaveTypeDto leaveTypeDto { get; set; } = null!;
    }
}
