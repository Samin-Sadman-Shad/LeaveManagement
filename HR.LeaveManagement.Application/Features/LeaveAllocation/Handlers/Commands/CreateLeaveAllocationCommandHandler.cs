using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.DTO.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagement.Application.Responses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using AutoMapper;
using entities = HR.LeaveManagement.Domain.Entities;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    /// <summary>
    /// Create a new leave allocation of a Leave Type and allocate it for all the employees in bulk
    /// </summary>
    public class CreateLeaveAllocationsCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, CreateCommandResponse<CreateLeaveAllocationDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationsCommandHandler(ILeaveTypeRepository leaveTypeRepository, IUserService userService, 
            IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateCommandResponse<CreateLeaveAllocationDto>> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCommandResponse<CreateLeaveAllocationDto>();
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository, _userService);
            var validationResult = await validator.ValidateAsync(request.dto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Errors = validationResult.Errors.Select(failure => failure.ErrorMessage).ToList();
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            /*            var leaveAllocation = _mapper.Map<entities.LeaveAllocation>(request.dto);
                        leaveAllocation = await _leaveAllocationRepository.AddAsync(leaveAllocation);*/

            var leaveType = await _leaveTypeRepository.GetAsync(request.dto.LeaveTypeId);
            var employees = await _userService.GetEmployeesAsync();
            var numberOfDays = leaveType.AllocatedDays;

            var leaveAllocations = new List<entities.LeaveAllocation>();
            foreach(var employee in employees)
            {
                if(await _leaveAllocationRepository.LeaveAllocationExists(employee.Id, leaveType.Id, numberOfDays))
                {
                    continue;
                }

                var leaveAllocation = new entities.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = numberOfDays
                };

                leaveAllocations.Add(leaveAllocation);
            }

            await _leaveAllocationRepository.AddLeaveAllocations(leaveAllocations);

            response.Success = true;
            response.Message = $"Leave Allocation of {leaveType} to all the employees is successful";
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
