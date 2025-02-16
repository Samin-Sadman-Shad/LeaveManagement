using HR.LeaveManagement.Application.Contracts.Infrastrcuture;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //we want configuration related to EmailSetting
            //return a chunk of configuration that looks like email settings
            //EmailSetting will be bind against this chunk of configuration
            //it will look into the section of appSettings with name EmailSettings
            //the section will be formulated into what the class look like
            //retrive the EmailSetting data from this section and serialize into that class
            //services.ConfigureAndBind<EmailSetting>(configuration, "EmailSetting");
            //services.Configure<EmailSetting>(configuration.GetSection("EmailSection"));
            EmailSettingOptions emailSetting = new EmailSettingOptions();
            var section = configuration.GetSection(EmailSettingOptions.EmailSetting);
            section.Bind(emailSetting);
            services.AddSingleton(emailSetting);
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }

        public static IServiceCollection ConfigureAndBind<T>(
                this IServiceCollection services,
                IConfiguration configuration,
                string sectionName) where T : class, new()
        {
            var section = configuration.GetSection(sectionName);
            if (!section.Exists()) return services;

            var settings = new T();
            foreach (var child in section.GetChildren())
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                PropertyInfo property = typeof(T).GetProperty(child.Key);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (property != null && property.CanWrite)
                {
                    object convertedValue = Convert.ChangeType(child.Value, property.PropertyType);
                    property.SetValue(settings, convertedValue);
                }
            }

            services.Configure<T>(section);
            return services;
        }
    }
}
