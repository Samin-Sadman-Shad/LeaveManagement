using FluentValidation;

namespace HR.LeaveManagement.MVC.Models
{
    public class LoginVMValidator:AbstractValidator<LoginViewModel>
    {
        public LoginVMValidator() 
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email can not be empty")
                .EmailAddress().WithMessage("Please provide a valid email");

            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password can not be empty");
/*                .MinimumLength(3).WithMessage("Minimum 3 characters needed");*/
        }
    }
}
