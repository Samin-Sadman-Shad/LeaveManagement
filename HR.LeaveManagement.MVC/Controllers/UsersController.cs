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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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
            //already created the user session and stored the jwt in local memory
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

            string returnUrl = Url.Content("~/");
            var isRegistered = await _authenticationService.Register(viewModel);
            if (isRegistered)
            {
                //registration is successful
                return LocalRedirect(returnUrl);
            }

            return View(viewModel);
        }
    }
}
