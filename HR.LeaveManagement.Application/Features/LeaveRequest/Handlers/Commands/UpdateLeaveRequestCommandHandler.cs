using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.DTO.LeaveRequest.Validators;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
/*        ILeaveRequestRepository _leaveRequestRepository;
        ILeaveTypeRepository _leaveTypeRepository;
        ILeaveAllocationRepository _leaveAllocationRepository;*/
        IMapper _mapper;
        IUnitOfWork _unitOfWork;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository repository, ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
/*            _leaveRequestRepository = repository;
            _leaveTypeRepository = leaveTypeRepo;
            _leaveAllocationRepository = leaveAllocationRepository;*/
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            //retrieve the leave request object from db
            //var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id);
            if(leaveRequest == null)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            //check the type of request by the Dto
            if (request.UpdateLeaveRequestDto != null)
            {
                //var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);
                if(!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    return response;
                }
                //update the destination entity with corresponding value coming from request
                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                //entity is updated, put back it to db
                //leaveRequest = await _leaveRequestRepository.UpdateAsync(leaveRequest);
                leaveRequest = await _unitOfWork.LeaveRequestRepository.UpdateAsync(leaveRequest);
                await _unitOfWork.SaveAsync();
            }
            //multiple updates in the same section, multiple write operation, so use UnitOfWork
            else if (request.ChangeLeaveRequestApprovalDto != null) 
            {
                //update the destination entity with corresponding value coming from request
                //_mapper.Map(request.ChangeLeaveRequestApprovalDto, leaveRequest);
                //await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.IsApproved);
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.IsApproved);

                //if the leave request is approved
                //deduct the approved days of leave from the allocated leave days for that type and user id
                if (request.ChangeLeaveRequestApprovalDto.IsApproved is not null 
                    && request.ChangeLeaveRequestApprovalDto.IsApproved.Value)
                {
                    /*                    var allocation = await _leaveAllocationRepository.
                                            GetLeaveAllocationByUserIdWithLeaveType(leaveRequest.EmployeeId, leaveRequest.LeaveTypeId);
                                        var approvedDays = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;*/
                    var allocation = await _unitOfWork.LeaveAllocationRepository.
                                     GetLeaveAllocationByUserIdWithLeaveType(leaveRequest.EmployeeId, leaveRequest.LeaveTypeId);
                    var approvedDays = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                    allocation.NumberOfDays -= approvedDays;
                    /*                    await _leaveAllocationRepository.UpdateAsync(allocation);*/
                    await _unitOfWork.LeaveAllocationRepository.UpdateAsync(allocation);
                }
                await _unitOfWork.SaveAsync();
            }
            //return Unit.Value;
            return response;
        }
    }
}
