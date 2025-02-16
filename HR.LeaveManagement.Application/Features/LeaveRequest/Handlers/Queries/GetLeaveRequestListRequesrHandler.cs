using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveRequestListRequesrHandler : IRequestHandler<GetLeaveRequestListRequest, BaseQueryListResponse<LeaveRequestListDto>>
    {
        public readonly ILeaveRequestRepository _leaveRequestRepository;
        public readonly IMapper _mapper;

        public GetLeaveRequestListRequesrHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<BaseQueryListResponse<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryListResponse<LeaveRequestListDto>();
            var leaveRequests = await _leaveRequestRepository.GetAllAsync();
            if(leaveRequests is null )
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                return response;
            }
            if(leaveRequests.Count() == 0)
            {
                response.Success = true;
                response.StatusCode=System.Net.HttpStatusCode.NotFound;
                //response.Records = new List<BaseQueryDto>();
                response.Records = new List<LeaveRequestListDto>();
                return response;
            }
            var records = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            response.Success = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            //response.Records = records.Cast<BaseQueryDto>().ToList();
            response.Records = records;

            return response;
        }
    }
}
