using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public async Task<bool> CheckUserExists(string EmployeeId)
        {
            throw new NotImplementedException();
        }
    }
}
