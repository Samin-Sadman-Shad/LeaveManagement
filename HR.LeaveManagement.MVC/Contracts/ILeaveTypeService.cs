using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        public Task<List<LeaveTypeViewModel>> GetAll();
        public Task<LeaveTypeViewModel> GetById(int id);
        public Task<Response<int>> Create(CreateLeaveTypeViewModel model);

        public Task<Response<int>> Update(int id, LeaveTypeViewModel model);
        public Task<Response<int>> Delete(int id);
    }
}
