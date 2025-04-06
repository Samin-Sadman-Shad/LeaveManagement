using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class CreateLeaveRequestViewModel
    {
        [Display(Name ="Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Comments")]
        [Required]
        [MaxLength(300)]
        public string? RequestComment { get; set; }
        [Display(Name ="Leave Type Id")]
        [Required]
        public int LeaveTypeId { get; set; }

        //will be rendered as html <select> items to select from
        public SelectList? LeaveTypes { get; set; }
    }
}
