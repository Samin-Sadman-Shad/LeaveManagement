using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Infrastrcucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrstructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //we want configuration related to EmailSetting
            //return a chunk of configuration that looks like email settings
            //EmailSetting will be bind against this chunk of configuration
            //it will look into the section of appSettings with name EmailSettings
            //the section will be formulated into what the class look like
            //retrive the EmailSetting data from this section and serialize into that class
            services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            return services;
        }
    }
}
