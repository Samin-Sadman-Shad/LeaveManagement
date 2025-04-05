using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class RegistrationViewModel
    {
        [MaxLength(15)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(10)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

/*        public string UserName { get; set; }*/
    }
}
