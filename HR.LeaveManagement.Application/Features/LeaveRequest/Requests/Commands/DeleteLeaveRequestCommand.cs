using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class DeleteLeaveRequestCommand:IRequest<BaseCommandResponse>
    {
        public DeleteLeaveRequestDto DeleteLeaveRequestDto { get; set; }
    }
}
