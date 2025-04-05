using HR.LeaveManagement.Application.Contracts.Authentication;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckUserExists(string employeeId)
        {
            var result = await GetEmployeeByIdAsync(employeeId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            var result = users.Select(user => new Employee
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            }).Where( employee => employee.Id == id).FirstOrDefault();
            return result;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            var result = users.Select(user => new Employee
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            }).ToList();

            return result;
        }
    }
}
