using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Persistance.Contracts
{
    internal interface ILeaveAllocationRepository:IGenericRepository<LeaveAllocation>
    {
    }
}
