using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationDtoValidator:AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository repository;

        public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository _repository) 
        {
            this.repository = _repository;
            Include( new ILeaveAllocationDtoValidator(repository) );

/*            RuleFor(dto => dto.Id)
                .NotNull().WithMessage("{PorpertyName} must be present");*/
        }
    }
}
