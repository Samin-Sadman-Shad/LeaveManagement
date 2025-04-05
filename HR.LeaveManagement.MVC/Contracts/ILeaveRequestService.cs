using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestViewModel viewModel);
        Task<Response<bool>> DeleteLeaveRequest(int leaveRequestId);
    }
}
