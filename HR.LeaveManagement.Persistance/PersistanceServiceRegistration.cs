
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistsanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LeaveManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            return services;
        }
    }
}
