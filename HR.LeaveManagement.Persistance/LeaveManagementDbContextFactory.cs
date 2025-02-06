using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance
{
    public class LeaveManagementDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementDbContext>
    {
        public LeaveManagementDbContext CreateDbContext(string[] args)
        {
            //provide the configuration to build out the service
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<LeaveManagementDbContext>();
            //var connectionString = configuration.GetConnectionString("LeaveManagementConnectionString");

            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new InvalidOperationException("⚠ Connection string 'LeaveManagementConnection' is null or empty. Check appsettings.json.");
            //}

            builder.UseSqlServer("Server=SAMINSHAD186;Database=LeaveManagement;User=admin;Password=admin;");
            return new LeaveManagementDbContext(builder.Options);
        }
    }
}
