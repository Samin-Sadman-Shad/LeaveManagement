using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var validator = new LoginVMValidator();
            var validationResult = await validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                ModelState.AddModelError("", "Login Failed. Please try again!");
            }
            var isLoggedIn = await _authenticationService.Authenticate(viewModel.Email, viewModel.Password);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }
            return View(viewModel);
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Registration failed");
            }

            var isRegistered = await _authenticationService.Authenticate(viewModel.Email, viewModel.Password);
            if (isRegistered)
            {
                //registration is successful
                return View();
            }

            return View(viewModel);
        }
    }
}
