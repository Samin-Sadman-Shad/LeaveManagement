﻿using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance.Repositories
{
    /// <summary>
    /// Used by the HR to add leave allocation to employees, get data for leave allocations for employees
    /// </summary>
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
        {
            
        }

        public async Task AddLeaveAllocation(LeaveAllocation leaveAllocation)
        {
            await _dbContext.AddAsync(leaveAllocation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddLeaveAllocations(List<LeaveAllocation> leaveAllocations)
        {
            await _dbContext.AddRangeAsync(leaveAllocations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LeaveAllocation>> GetAllLeaveAllocationsWithDetails()
        {
            return await _dbContext.leaveAllocations
                .Include<LeaveAllocation, LeaveType>(l => l.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByUserIdWithLeaveType(string employeeId, int leaveTypeId)
        {
            var allocation = await _dbContext.leaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId
                                                    && q.LeaveTypeId == leaveTypeId);
            return allocation;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var result = await _dbContext.leaveAllocations
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(l => l.Id == id);
            return result;
        }

        public async Task<bool> LeaveAllocationExists(string employeeId, int leaveTypeId, int numberOfDays)
        {
            return await _dbContext.leaveAllocations.AnyAsync(q => q.EmployeeId == employeeId 
                                                                && q.NumberOfDays == numberOfDays 
                                                                && q.LeaveTypeId == leaveTypeId);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsByUserId(string userId)
        {
            var leaveAllocations = await _dbContext.leaveAllocations.Where(q => q.EmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();
            return leaveAllocations;
        }
    }
}
