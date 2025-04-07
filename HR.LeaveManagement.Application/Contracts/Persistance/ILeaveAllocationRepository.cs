using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface ILeaveAllocationRepository:IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetAllLeaveAllocationsWithDetails();

        Task<bool> LeaveAllocationExists(string employeeId, int leaveTypeId, int numberOfDays);

        Task<LeaveAllocation> GetLeaveAllocationByUserIdWithLeaveType(string employeeId, int leaveTypeId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsByUserId(string userId);

        Task AddLeaveAllocations(List<LeaveAllocation> leaveAllocations);

        Task AddLeaveAllocation(LeaveAllocation leaveAllocation);
    }
}
