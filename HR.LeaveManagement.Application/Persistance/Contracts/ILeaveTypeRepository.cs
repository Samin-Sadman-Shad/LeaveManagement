﻿using HR.LeaveManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Persistance.Contracts
{
    public interface ILeaveTypeRepository:IGenericRepository<LeaveType>
    {
    }
}
