using HR.LeaveManagement.Application.DTO.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    internal class CreateLeaveRequestCommand:IRequest<int>
    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; } = null!;
    }
}
