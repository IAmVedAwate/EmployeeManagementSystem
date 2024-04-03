using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Models
{ 
    public class EmployeeModelView
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Select Department!!")]
        public Dept? Department { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
