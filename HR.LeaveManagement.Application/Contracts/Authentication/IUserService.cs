using HR.LeaveManagement.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Authentication
{
    //specialized user operation
    public interface IUserService
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<bool> CheckUserExists(string employeeId);

        Task<Employee> GetEmployeeByIdAsync(string id);
    }
}
