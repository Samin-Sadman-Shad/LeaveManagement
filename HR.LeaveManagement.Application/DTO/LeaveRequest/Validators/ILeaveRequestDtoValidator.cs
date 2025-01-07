using FluentValidation;
using HR.LeaveManagement.Application.Persistance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator:AbstractValidator<ILeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _repository;

        public ILeaveRequestDtoValidator(ILeaveTypeRepository repository)
        {
            this._repository = repository;

            RuleFor(dto => dto.StartDate)
                .GreaterThanOrEqualTo(DateTime.Now)
                .LessThan(dto => dto.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(dto => dto.EndDate)
                .GreaterThan(dto => dto.StartDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(dto => dto.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _repository.Exists(id);
                    return leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exists");
        }
    }
}
