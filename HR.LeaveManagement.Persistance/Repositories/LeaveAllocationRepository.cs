﻿using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
        {
            
        }
        public async Task<List<LeaveAllocation>> GetAllLeaveAllocationsWithDetails()
        {
            return await _dbContext.leaveAllocations
                .Include<LeaveAllocation, LeaveType>(l => l.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _dbContext.leaveAllocations
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
