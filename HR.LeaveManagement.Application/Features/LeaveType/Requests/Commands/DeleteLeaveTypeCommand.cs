using HR.LeaveManagement.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands
{
    public class DeleteLeaveTypeCommand:IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
