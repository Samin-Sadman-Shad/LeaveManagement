using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDto? UpdateLeaveRequestDto { get; set; }
        public ChangeLeaveRequestApprovalDto? ChangeLeaveRequestApprovalDto { get; set; }
    }
}
