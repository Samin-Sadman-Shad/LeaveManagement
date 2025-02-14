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
        ILeaveRequestRepository _leaveRequestRepository;
        ILeaveTypeRepository _leaveTypeRepository;
        IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository repository, ILeaveTypeRepository leaveTypeRepo IMapper mapper)
        {
            _leaveRequestRepository = repository;
            _leaveTypeRepository = leaveTypeRepo;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse(); 

            //retrieve the leave request object from db
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);
            if(leaveRequest == null)
            {
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            //check the type of request by the Dto
            if (request.UpdateLeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
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
                leaveRequest = await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null) 
            {
                //update the destination entity with corresponding value coming from request
                //_mapper.Map(request.ChangeLeaveRequestApprovalDto, leaveRequest);
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.IsApproved);
            }
            //return Unit.Value;
            return response;
        }
    }
}
