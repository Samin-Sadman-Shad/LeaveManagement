using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
/*            await _dbContext.SaveChangesAsync();*/
        }

        public async Task<IReadOnlyList<LeaveRequest>> GetAllLeaveRequestsWithDetail()
        {
            return await _dbContext.leaveRequests
               .Include(l => l.LeaveType)
               .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetail(int id)
        {
            var leaveRequest = await _dbContext.leaveRequests
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(l => l.Id == id);
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithUserId(string userId)
        {
            var leaveRequest = await _dbContext.leaveRequests.Where(leaveRequest => leaveRequest.EmployeeId == userId)
                .Include(leaveRequest => leaveRequest.LeaveType)
                .ToListAsync();
            return leaveRequest;
        }
    }
}
