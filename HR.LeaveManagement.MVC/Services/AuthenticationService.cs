using HR.LeaveManagement.Application.Models.Identity;
using contracts = HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
/*using Microsoft.AspNetCore.Identity.Data;*/
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, contracts.IAuthenticationService
    {
        //allows to access the context of http request
        private readonly IHttpContextAccessor _httpContextAccessor;
        //will not be injected, will be initialized in constructor
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor
            , contracts.ILocalStorageService localStorage, IClient client):base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Calls the api for login internally and receives the authResponse with token.
        /// Then extracts the user claims from the token and save it to local storage
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest request = new AuthRequest { Email = email, Password = password };

                var authResponse = await _client.LoginAsync(request);
                if (authResponse != null && authResponse.Token is not null)
                {
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
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
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

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
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
        }

        IList<Claim> ParseToken(string token)
        {
            JwtSecurityToken jwt = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var claims = jwt.Claims.ToList();
            return claims;
        }
    }
}
