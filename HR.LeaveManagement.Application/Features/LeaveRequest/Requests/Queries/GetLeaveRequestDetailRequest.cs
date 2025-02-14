using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Responses.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveRequestDetailRequest:IRequest<LeaveRequestDtoQueryResponse>
    {
        public int Id  { get; set; }
    }
}
