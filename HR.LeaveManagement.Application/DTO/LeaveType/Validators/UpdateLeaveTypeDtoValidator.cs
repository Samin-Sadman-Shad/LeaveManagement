﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTO.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator:AbstractValidator<UpdateLeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator()
        {
            //RuleFor(dto => dto.LeaveTypeName)
            //    .NotEmpty().WithMessage("{PropertyName} can not be empty")
            //    .NotNull().WithMessage("{PropertyName} can not be null")
            //    .MaximumLength(50).WithMessage("{PropertyName} can not exceed 50 characters");

            //RuleFor(dto => dto.AllocatedDays)
            //    .NotEmpty().WithMessage("{PropertyName} can not be empty")
            //    .GreaterThan(0).WithMessage("{PropertyName} must be at least 1")
            //    .LessThan(100).WithMessage("{PropertyName} can not exceed 100");
            Include(new ILeaveTypeDtoValidator());

/*            RuleFor(dto => dto.Id)
                .NotNull().WithMessage("{PropertyName} must be present");*/
        }

    }
}
