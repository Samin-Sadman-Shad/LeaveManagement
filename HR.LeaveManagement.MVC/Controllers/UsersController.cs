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
        /// don't use any argument to HttpPost
        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel viewModel, string returnUrl)
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
            if (isLoggedIn.IsSuccessful)
            {
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError(string.Empty, isLoggedIn.Error ?? "Login failed");
            return View(viewModel);
            
        }

        public IActionResult Register()
        {
            return View();
        }

        //don't use any argument to HttpPost
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Registration failed");
            }

            string returnUrl = Url.Content("~/");
            var isRegistered = await _authenticationService.Register(viewModel);
            if (isRegistered.IsSuccessful)
            {
                //registration is successful
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, isRegistered.Error ?? "Registration failed");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authenticationService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
