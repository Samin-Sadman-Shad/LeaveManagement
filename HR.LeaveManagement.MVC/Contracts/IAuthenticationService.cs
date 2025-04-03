using HR.LeaveManagement.MVC.Models;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);

        Task<bool> Register(string firstName, string lastName, string userName, string email, string password);

        //don't pass too many parameters
        Task<bool> Register(RegistrationViewModel viewModel);

        Task Logout();
    }
}
