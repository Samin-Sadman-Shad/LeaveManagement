using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class CreateLeaveRequestCommand:IRequest<CreateCommandResponse>
    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; } = null!;
    }
}
