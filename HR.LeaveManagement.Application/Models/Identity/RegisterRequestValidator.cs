using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Models.Identity
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(req => req.FirstName).
                MaximumLength(20).
                WithMessage("Please enter less than 20 characters");

            RuleFor(req => req.LastName).
                MaximumLength(20).
                WithMessage("Please enter less than 20 characters");

            RuleFor(req => req.Email)
                .EmailAddress()
                .WithMessage("Please enter a valid email address");

            RuleFor(req => req.Password).MinimumLength(5).WithMessage("Please enter at least 5 characters");
                
        }
    }
}
