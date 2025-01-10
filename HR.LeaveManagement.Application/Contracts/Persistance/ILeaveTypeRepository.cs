using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface ILeaveTypeRepository:IGenericRepository<LeaveType>
    {
    }
}
