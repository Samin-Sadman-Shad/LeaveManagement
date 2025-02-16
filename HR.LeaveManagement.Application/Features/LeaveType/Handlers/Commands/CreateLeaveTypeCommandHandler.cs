using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistance;
using entity = HR.LeaveManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTO.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using System.Net;
using HR.LeaveManagement.Application.Utils;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, CreateCommandResponse<CreateLeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _leaveTypeRepository = repository;
            _mapper = mapper;
        }

        public async Task<CreateCommandResponse<CreateLeaveTypeDto>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCommandResponse<CreateLeaveTypeDto>()    ;
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto, cancellationToken);
            if (!validationResult.IsValid) 
            {
                //throw new Exceptions.ValidationException(validationResult);
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = ValidationUtils.AddValidationErrorsToResponse(validationResult);
            }
            var leaveType = _mapper.Map<entity.LeaveType>(request.leaveTypeDto);
            leaveType = await _leaveTypeRepository.AddAsync(leaveType);

            response.Success = true;
            response.StatusCode = HttpStatusCode.Created;
            response.Message = "New Leave type created Successfully";
            response.RecordId = leaveType.Id;
            response.Record = request.leaveTypeDto;
            //return leaveType.Id;
            return response;
        }
    }
}
