using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Domain.Entities;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, BaseQueryResponse>

    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository repository, IMapper mapper)
        {
            _leaveRequestRepository = repository;
            _mapper = mapper;
        }
        public async Task<BaseQueryResponse> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryResponse();
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);
            if (leaveRequest == null) 
            {
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            //return _mapper.Map<LeaveRequestDto>(leaveRequest);
            var record = _mapper.Map<LeaveRequestDto>(leaveRequest);
            response.Success = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Record = record;
            return response;
        }
    }
}
