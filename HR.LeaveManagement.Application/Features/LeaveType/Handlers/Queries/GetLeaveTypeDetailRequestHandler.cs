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
using System.Net;
using HR.LeaveManagement.Application.Responses.LeaveType;
using HR.LeaveManagement.Application.Responses.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, BaseQueryResponse<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _leaveTypeRepository = repository;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse<LeaveTypeDto>> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryResponse<LeaveTypeDto>();
            //response.RecordId = request.Id;
            var leaveTypeDetail = await _leaveTypeRepository.GetAsync(request.Id);
            if(leaveTypeDetail == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                //response.Message = "Leave type not found";
                response.Message = "Leave type not found";
            }
            var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveTypeDetail);

            response.StatusCode = HttpStatusCode.OK;
            response.Record = leaveTypeDto;

            return response;
        }
    }
}
