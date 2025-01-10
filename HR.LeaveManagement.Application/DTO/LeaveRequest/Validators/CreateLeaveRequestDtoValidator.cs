using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator:AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestDtoValidator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            Include(new ILeaveRequestDtoValidator(repository));

            //RuleFor(dto => dto.StartDate)
            //    .GreaterThanOrEqualTo(DateTime.Now)
            //    .LessThan(dto => dto.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            //RuleFor(dto => dto.EndDate)
            //    .GreaterThan(dto => dto.StartDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            //RuleFor(dto => dto.LeaveTypeId)
            //    .GreaterThan(0)
            //    .MustAsync(async (id, token) =>
            //    {
            //        var leaveTypeExists = await _repository.Exists(id);
            //        return leaveTypeExists;
            //    })
            //    .WithMessage("{PropertyName} does not exists");


        }

        public ILeaveTypeRepository _repository { get; }
    }
}
