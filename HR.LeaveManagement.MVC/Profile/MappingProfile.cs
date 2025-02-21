
using AutoMapper;
using HR.LeaveManagement.Domain.Entities;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using automapper = AutoMapper;

namespace HR.LeaveManagement.MVC.Profile
{
    public class MappingProfile:automapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeViewModel>();
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeViewModel>();
            CreateMap<UpdateLeaveTypeDto, LeaveTypeViewModel>();
        }

        
    }
}
