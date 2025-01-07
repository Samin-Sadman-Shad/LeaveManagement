using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    internal class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        ILeaveRequestRepository _leaveRequestRepository;
        IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository repository, IMapper mapper)
        {
            _leaveRequestRepository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            //var validator = 

            //retrieve the leave request object from db
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);
            //check the type of request by the Dto
            if (request.UpdateLeaveAllocationDto != null)
            {
                //update the destination entity with corresponding value coming from request
                _mapper.Map(request.UpdateLeaveAllocationDto, leaveRequest);
                //entity is updated, put back it to db
                leaveRequest = await _leaveRequestRepository.UpdateLeaveRequest(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null) 
            {
                //update the destination entity with corresponding value coming from request
                //_mapper.Map(request.ChangeLeaveRequestApprovalDto, leaveRequest);
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.IsApproved);
            }
            return Unit.Value;
        }
    }
}
