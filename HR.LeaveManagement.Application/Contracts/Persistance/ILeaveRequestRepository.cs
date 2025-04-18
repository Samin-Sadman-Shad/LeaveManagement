﻿using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface ILeaveRequestRepository:IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetail(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithUserId(string userId);
        Task<IReadOnlyList<LeaveRequest>> GetAllLeaveRequestsWithDetail();
        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? status);
    }
}
