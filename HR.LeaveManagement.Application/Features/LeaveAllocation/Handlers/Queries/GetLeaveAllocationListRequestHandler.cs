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
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Constants;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    internal class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, BaseQueryListResponse<LeaveAllocationDto>>
    {
        ILeaveAllocationRepository _leaveAllocationRepository;
        IMapper _mapper;
        IHttpContextAccessor _httpContextAccessor;
        IUserService _userService;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository repository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _mapper = mapper;
            _leaveAllocationRepository = repository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public async Task<BaseQueryListResponse<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryListResponse<LeaveAllocationDto>();
            try
            {
                if (request.isLoggedIn)
                {
                    var userId =  _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == CustomClaimTypes.Uid)?.Value;
                    var employee = await _userService.GetEmployeeByIdAsync(userId);
                    var leaveAllocations =  await _leaveAllocationRepository.GetLeaveAllocationsByUserId(userId);

                    if (leaveAllocations is null)
                    {
                        response.Success = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        response.Message = "Error occurred while fetching Leave Allocations";
                        return response;
                    }
                    if (leaveAllocations.Count == 0)
                    {
                        response.Success = true;
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = "No Leave Allocations Found";
                        return response;
                    }

                    var records = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

                    foreach(var dto in records)
                    {
                        dto.Employee = employee;
                    }

                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Records = records;
                    response.Message = "Leave Allocation data fetched successfully";
                }
                else
                {
                    var leaveAllocations = await _leaveAllocationRepository.GetAllLeaveAllocationsWithDetails();
                    if (leaveAllocations is null)
                    {
                        response.Success = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        response.Message = "Error occurred while fetching Leave Allocations";
                        return response;
                    }
                    if (leaveAllocations.Count == 0)
                    {
                        response.Success = true;
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = "No Leave Allocations Found";
                        return response;
                    }

                    var records = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Records = records;
                    response.Message = "Leave Allocation data fetched successfully";
                    
                }
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.ExpectationFailed;
                response.Message = ex.Message;
                return response;
            }

        }
    }
}
