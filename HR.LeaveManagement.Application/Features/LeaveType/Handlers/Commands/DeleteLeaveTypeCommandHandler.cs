using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
    internal class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _leaveTypeRepository = repository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveType = await _leaveTypeRepository.GetAsync(request.Id);
            if (leaveType == null) 
            {
                //throw new NotFoundException(nameof(leaveType), request.Id);
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            await _leaveTypeRepository.DeleteAsync(request.Id);
            response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return response;

        }
    }
}
