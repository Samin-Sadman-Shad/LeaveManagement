using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator:AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly IUserService _userService;
        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;

            Include(new ILeaveAllocationDtoValidator(repository));

            RuleFor(dto => dto.EmployeeId).NotNull()
                .MustAsync(async (id, token) =>
                {
                    var employeeExists = await _userService.CheckUserExists(id);
                    return employeeExists;
                }).WithMessage("{PropertyName} does not exists");

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
