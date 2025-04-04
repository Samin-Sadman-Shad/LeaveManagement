using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface IUserRepository
    {
        Task<bool> CheckUserExists(string EmployeeId);
    }
}
