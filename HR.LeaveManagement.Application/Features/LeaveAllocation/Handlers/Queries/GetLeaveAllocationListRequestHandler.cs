using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HR.LeaveManagement.Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    internal class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        ILeaveAllocationRepository _leaveAllocationRepository;
        IMapper _mapper;
        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _leaveAllocationRepository = repository;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetAllLeaveAllocationsWithDetails();
            return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        }
    }
}
