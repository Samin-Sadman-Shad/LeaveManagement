using HR.LeaveManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Infrastrcuture
{
    public interface IEmailSender
    {
        public Task<bool> SendEmailAsync(Email email);
    }
}
