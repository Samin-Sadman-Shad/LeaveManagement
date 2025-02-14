using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.Responses.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class CreateLeaveRequestCommand:IRequest<CreateLeaveRequestDtoCommandResponse>
    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; } = null!;
    }
}
