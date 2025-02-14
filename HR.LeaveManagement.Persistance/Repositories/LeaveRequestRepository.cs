using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
        {
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? status)
        {
            leaveRequest.Approved = status;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<LeaveRequest>> GetAllLeaveRequestsWithDetail()
        {
            return await _dbContext.leaveRequests
               .Include(l => l.LeaveType)
               .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetail(int id)
        {
            return await _dbContext.leaveRequests
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

    }
}
