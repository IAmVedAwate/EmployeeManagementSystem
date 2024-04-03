using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Select Department!!")]
        public Dept? Department { get; set; }
        public string? PhotoPath { get; internal set; }
        /*        public string PhotoPath { get; set; }*/
    }
}
