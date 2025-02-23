
using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace HR.LeaveManagement.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettingOptions jwtSettingOptions = new JwtSettingOptions();
            var section = configuration.GetSection(JwtSettingOptions.jwtSettings);
            section.Bind(jwtSettingOptions);
            services.AddSingleton(jwtSettingOptions);

            services.AddDbContext<LeaveManagementIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LeaveManagementConnectionString"),
                    sqlOptionBuilder =>
                    {
                        sqlOptionBuilder.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName);
                    });
            });
            //services.AddSingleton<IAuthService, AuthService>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LeaveManagementIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });
            return services;
        }
    }
}
