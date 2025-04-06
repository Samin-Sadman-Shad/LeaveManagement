using AutoMapper;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetLeaveRequestListRequesrHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper,
            IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BaseQueryListResponse<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryListResponse<LeaveRequestListDto>();

            //logged in as a user and see all of user's request
            if (request.IsLoggedInUser)
            {
                //get the userId from the HttpContext
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == CustomClaimTypes.Uid)?.Value;

                if(userId == null)
                {
                    response.Success = false;
                    response.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                    return response;
                }

                //fetch only the leave request created by this user
                var leaveRequests =  await _leaveRequestRepository.GetLeaveRequestsWithUserId(userId);

                if (leaveRequests is null)
                {
                    response.Success = false;
                    response.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                    return response;
                }
                if (leaveRequests.Count() == 0)
                {
                    response.Success = true;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    //response.Records = new List<BaseQueryDto>();
                    response.Records = new List<LeaveRequestListDto>();
                    return response;
                }

                var records = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                var employee =  await _userService.GetEmployeeByIdAsync(userId);
                foreach(var dto in records)
                {
                    dto.Employee = employee;
                }

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Records = records;
            }
            else
            {
                var leaveRequests = await _leaveRequestRepository.GetAllAsync();
                if (leaveRequests is null)
                {
                    response.Success = false;
                    response.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                    return response;
                }
                if (leaveRequests.Count() == 0)
                {
                    response.Success = true;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    //response.Records = new List<BaseQueryDto>();
                    response.Records = new List<LeaveRequestListDto>();
                    return response;
                }
                var records = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                //for administrator, there are different employees
                //for each leaveRequest fetch the employee created it by their id in leaveRequest
                //assign that employee to the dto
                foreach (var dto in records)
                {
                    dto.Employee = await _userService.GetEmployeeByIdAsync(dto.EmployeeId);
                }

                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                //response.Records = records.Cast<BaseQueryDto>().ToList();
                response.Records = records;
            }

            

            return response;
        }
    }
}
