using HR.LeaveManagement.Domain.Entities;

namespace HR.LeaveManagement.MVC.Models
{
    public class EmployeeLeaveRequestViewModel
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
}
