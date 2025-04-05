using HR.LeaveManagement.Application.Models.Identity;
using contracts = HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
/*using Microsoft.AspNetCore.Identity.Data;*/
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HR.LeaveManagement.MVC.Models;

namespace HR.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, contracts.IAuthenticationService
    {
        //allows to access the context of http request
        private readonly IHttpContextAccessor _httpContextAccessor;
        //will not be injected, will be initialized in constructor
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        /// <summary>
        /// Client and local storage services are already injected with their implemntation in program file
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="localStorage"></param>
        /// <param name="client"></param>
        public AuthenticationService(IHttpContextAccessor httpContextAccessor
            , contracts.ILocalStorageService localStorage, IClient client):base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Calls the api for login internally and receives the authResponse with token.
        /// Then extracts the user claims from the token and save it to local storage
        /// also login the user principle with a cookie 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AuthenticationServiceResponse> Authenticate(string email, string password)
        {
            var result = new AuthenticationServiceResponse();
            try
            {
                
                AuthRequest request = new AuthRequest { Email = email, Password = password };

                var authResponse = await _client.LoginAsync(request);
                if (authResponse != null && authResponse.Token is not null)
                {
                    result.Error = authResponse.AuthError;
                    //extract the token
                    var token = authResponse.Token;
                    //read and parse the claims from token
                    var claims = ParseToken(token);
                    //create user for the claims
                    //once user is created, store that user record, store user session as a cookie
                    //on client side user is a ClaimsPrincipal with claims
                    //storing that user session(user currently in the system) as a cookie
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    //let the user sign in, by accessing the context of http request which is the current request pipeline
                    //sign the user in using cookie authentication scheme
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.setStorageValue("token", token);
                    result.IsSuccessful = true;
                    return result;
                }
                result.IsSuccessful = false;
                return result;
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                return result ;
            }

        }

        /// <summary>
        /// Remove the Jwt from local storage and destroy the user cookies created during the sign in
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            _localStorage.ClearStorageValue("token");
            //implicitly destroy any user based cookies that were created during the sign in
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

/*        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {
            var registerReq = new RegisterRequest
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password

            };
            var response = await _client.RegisterAsync(registerReq);
            if(!String.IsNullOrEmpty(response.UserId))
            {
                return true;

            }
            return false;
        }*/

        public async Task<AuthenticationServiceResponse> Register(RegistrationViewModel vm)
        {
            var result = new AuthenticationServiceResponse();
            var registerReq = new RegisterRequest
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName =  vm.FirstName+vm.LastName,
                Email = vm.Email,
                Password = vm.Password

            };
            var response = await _client.RegisterAsync(registerReq);
            result.Error = response.RegisterError;
            if (!String.IsNullOrEmpty(response.UserId))
            {
                //log the user in after successful registration
                await Authenticate(vm.Email, vm.Password);
                result.IsSuccessful = true;
                return result;

            }
            return result;
        }

        IList<Claim> ParseToken(string token)
        {
            JwtSecurityToken jwt = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var claims = jwt.Claims.ToList();
            return claims;
        }
    }
}
