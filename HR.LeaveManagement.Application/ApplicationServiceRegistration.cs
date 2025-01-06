using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Register all the services in Application project
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
