using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationServiceResponse> Authenticate(string email, string password);

/*        Task<bool> Register(string firstName, string lastName, string userName, string email, string password);*/

        //don't pass too many parameters
        Task<AuthenticationServiceResponse> Register(RegistrationViewModel viewModel);

        Task Logout();
    }
}
