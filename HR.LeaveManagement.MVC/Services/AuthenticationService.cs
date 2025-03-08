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
            AuthRequest request = new AuthRequest { Email = email, Password = password };

            var authResponse = await _client.LoginAsync(request);
            if (authResponse != null && authResponse.Token is not null)
            {
                //extract the token
                var token = authResponse.Token;
                //read and parse the claims from token
                var claims = ParseToken(token);
                //create user for the claims
                //once user is created, store that user record store user session as a cookie
                //on client side user is a ClaimsPrincipal with claims
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                //let the user sign in
                var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.setStorageValue("token", token);
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorageValue("token");
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
