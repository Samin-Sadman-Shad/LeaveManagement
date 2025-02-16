using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses.Common;
using System.Net;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    internal class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, BaseQueryResponse<LeaveAllocationDto>>
    {
        ILeaveAllocationRepository _leaveAllocationRepository;
        IMapper _mapper;
        public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _leaveAllocationRepository = repository;
        }
        public async Task<BaseQueryResponse<LeaveAllocationDto>> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryResponse<LeaveAllocationDto>();
            try
            {
                
                var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
                if (leaveAllocation == null)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Leave Allocation data for id={request.Id} not found";
                    response.Record = null;
                    return response;
                }
                var record = _mapper.Map<LeaveAllocationDto>(leaveAllocation);
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Record = record;
                response.Message = $"Leave Allocation data found for id={request.Id}";
                return response;
            }
            catch(Exception ex)
            {
                response.Success= false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }

        }
    }
}
