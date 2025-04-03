
using AutoMapper;
using HR.LeaveManagement.Application.Models.Identity;
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
            CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeViewModel>().ReverseMap();
            CreateMap<UpdateLeaveTypeDto, LeaveTypeViewModel>().ReverseMap();

            CreateMap<RegistrationViewModel, RegisterRequest>().ReverseMap();
        }

        
    }
}
