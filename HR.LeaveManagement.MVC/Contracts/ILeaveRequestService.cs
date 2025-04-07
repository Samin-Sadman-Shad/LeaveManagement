using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Employee being able to ask for leave from this 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestViewModel viewModel);
        Task<LeaveRequestViewModel> GetLeaveRequestById(int id);
        Task<Response<bool>> DeleteLeaveRequest(int leaveRequestId);

        /// <summary>
        /// Admin being able to approve the leave request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approval"></param>
        /// <returns></returns>
        Task ApproveLeaveRequest(int id, bool approval);

        Task<AdminLeaveRequestViewModel> GetAdminLeaveRequestList();
    }
}
