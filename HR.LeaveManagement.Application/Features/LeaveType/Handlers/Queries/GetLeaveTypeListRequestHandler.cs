using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses;
using System.Net;
using System.Linq;
using HR.LeaveManagement.Application.DTO.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, LeaveTypeDtoQueryListResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _leaveTypeRepository = repository;
        }
        public async Task<LeaveTypeDtoQueryListResponse> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var response = new LeaveTypeDtoQueryListResponse();
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            if(leaveTypes is null ||  leaveTypes.Count == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Leave Types not found";
            }
            var records = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            response.StatusCode = HttpStatusCode.OK;
            response.Records = records;

            return response;
        }
    }
}
