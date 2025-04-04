using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator:AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _repository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository repository)
        {
            this._repository = repository;

            RuleFor(dto => dto.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _repository.Exists(id);
                    return leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exists");

            RuleFor(dto => dto.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(dto => dto.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be within or after {ComparisonValue}");
        }
    }
}
