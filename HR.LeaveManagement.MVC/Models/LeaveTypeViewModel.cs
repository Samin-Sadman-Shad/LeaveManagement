namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveTypeViewModel: CreateLeaveTypeViewModel
    {
        public int Id { get; set; }
    }

    public class CreateLeaveTypeViewModel
    {
        public string LeaveTypeName { get; set; }
        public int AllocatedDays { get; set; }
    }
}
