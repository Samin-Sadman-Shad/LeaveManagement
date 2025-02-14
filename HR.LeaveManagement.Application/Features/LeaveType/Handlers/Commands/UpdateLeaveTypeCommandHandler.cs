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
using HR.LeaveManagement.Application.Utils;
using HR.LeaveManagement.Application.Responses.Common;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _leaveTypeRepository = repository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto, cancellationToken);
            if (validationResult != null)
            {
                //throw new Exceptions.ValidationException(validationResult);
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Errors = ValidationUtils.AddValidationErrorsToResponse(validationResult);
                return response;
            }
            var leaveType = await _leaveTypeRepository.GetAsync(request.leaveTypeDto.Id);
            _mapper.Map(request.leaveTypeDto, leaveType);
            await _leaveTypeRepository.UpdateAsync(leaveType);

            response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }
    }
}
