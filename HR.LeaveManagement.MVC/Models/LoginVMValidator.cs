using FluentValidation;

namespace HR.LeaveManagement.MVC.Models
{
    public class LoginVMValidator:AbstractValidator<LoginViewModel>
    {
        public LoginVMValidator() 
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email can not be empty")
                .EmailAddress().WithMessage("Please provide a valid email");

            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password can not be empty")
                .MinimumLength(6).WithMessage("Minimum 6 characters needed");
        }
    }
}
