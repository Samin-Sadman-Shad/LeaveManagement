using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Persistance.Contracts;
using entities = HR.LeaveManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    internal class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>, 
    {
        ILeaveRequestRepository _leaveRequestRepository;
        IMapper _mapper;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository repository, IMapper mapper)
        {
            _leaveRequestRepository = repository;
            _mapper = mapper;

        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<entities.LeaveRequest>(request.CreateLeaveRequestDto);
            //EFCore will update the id
            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);
            return leaveRequest.Id;

        }
    }
}
