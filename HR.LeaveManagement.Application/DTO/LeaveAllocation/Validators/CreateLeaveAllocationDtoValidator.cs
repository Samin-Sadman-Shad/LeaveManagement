using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator:AbstractValidator<CreateLeaveAllocationDto>
    {
        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            Include(new ILeaveAllocationDtoValidator(repository));

            //RuleFor(dto => dto.LeaveTypeId)
            //    .GreaterThan(0)
            //    .MustAsync(async (id, token) =>
            //    {
            //        var leaveTypeExists = await _repository.Exists(id);
            //        return leaveTypeExists;
            //    })
            //    .WithMessage("{PropertyName} does not exists");

            //RuleFor(dto => dto.NumberOfDays)
            //    .GreaterThan(0);
            //RuleFor(dto => dto.Period)
            //    .GreaterThan(0);
        }

        public ILeaveTypeRepository _repository { get; }
    }
}
