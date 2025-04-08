using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        //register all the repositories through it
        ILeaveTypeRepository LeaveTypeRepository { get;  }
        ILeaveRequestRepository LeaveRequestRepository { get;  }
        ILeaveAllocationRepository LeaveAllocationRepository { get; }

        Task SaveAsync();

    }
}
