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
            JwtSettingOptions  jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;


        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                var authReponse = new AuthResponse
                {
                    AuthError = "No user with this email has registered"
                };
                return authReponse;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
            if(result is null)
            {
                var authReponse = new AuthResponse
                {
                    AuthError = "Incorrect password"
                };
                return authReponse;
            }
            if (result.Succeeded)
            {
                //user has successfully signed in
                //generate the token for the user
                var jwt = await GenerateToken(user);
                var authReponse = new AuthResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwt),

                };

                return authReponse;

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

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            ApplicationUser existingUser = null;
            if(request.UserName is not null)
            {
                existingUser = await _userManager.FindByNameAsync(request.UserName);
            }
             
            if(existingUser is not null)
            {
                var response = new RegisterResponse
                {
                    RegisterError = "User already exists with this user name!"
                };
                return response;
            }
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail is not null)
            {
                var response = new RegisterResponse
                {
                    RegisterError = "User already exists with this email!"
                };
                return response;
            }


            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName ?? request.FirstName + request.LastName,
            };
            var passwordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            user.PasswordHash = passwordHash;

            var result =  await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegisterResponse { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //generate user claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach(var role  in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
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
