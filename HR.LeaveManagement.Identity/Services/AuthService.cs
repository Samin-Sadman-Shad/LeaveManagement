using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettingOptions _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettingOptions>  jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;


        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
            if(result is null)
            {
                throw new NotImplementedException();
            }
            if (result.Succeeded)
            {

            }
            if(result.IsNotAllowed)
            {
                throw new NotImplementedException();
            }
            if(result.IsLockedOut)
            {
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //generate user claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach(var role  in roles)
            {
                roleClaims.Add(new Claim("role", role));
            }

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            //generate the token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials:signingCredentials);

            return token;
        }

    }
}
