using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _leaveTypeRepository = repository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto, cancellationToken);
            if (validationResult != null)
            {
                throw new Exceptions.ValidationException(validationResult);
            }
            var leaveType = await _leaveTypeRepository.GetAsync(request.leaveTypeDto.Id);
            _mapper.Map(request.leaveTypeDto, leaveType);
            await _leaveTypeRepository.UpdateAsync(leaveType);
            return Unit.Value;
        }
    }
}
