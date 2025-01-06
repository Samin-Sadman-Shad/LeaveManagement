using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries
{
    public class GetLeaveAllocationListRequest:IRequest<List<LeaveAllocationDto>>
    {
    }
}
