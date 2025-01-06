using AutoMapper;
using HR.LeaveManagement.Application.Persistance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features
{
    public abstract class BaseHandler
    {
        ILeaveAllocationRepository _leaveAllocationRepository;
        IMapper _mapper;

        protected BaseHandler(ILeaveAllocationRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _leaveAllocationRepository = repository;
        }
    }
}
