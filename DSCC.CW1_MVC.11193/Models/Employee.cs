using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DSCC.CW1_MVC._11193.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public virtual Department EmployeeDepartment { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        [Display(Name = "Department")]
        public int EmployeeDepartmentId { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
