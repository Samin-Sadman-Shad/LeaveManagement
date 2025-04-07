using HR.LeaveManagement.Application.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveRequestViewModel
    {
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Comments")]
        public string? RequestComment { get; set; }
        [Display(Name = "Leave Type Id")]
        [Required]
        public int LeaveTypeId { get; set; }

        public LeaveTypeViewModel LeaveType { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
