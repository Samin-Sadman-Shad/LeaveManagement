using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetLeaveRequestListRequest:IRequest<BaseQueryListResponse>
    {

    }
}
