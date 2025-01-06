using HR.LeaveManagement.Application.DTO.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveRequestDetailRequest:IRequest<LeaveRequestDto>
    {
        public int Id  { get; set; }
    }
}
