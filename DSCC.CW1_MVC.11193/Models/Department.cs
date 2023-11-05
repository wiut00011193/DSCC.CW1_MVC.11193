using System.ComponentModel.DataAnnotations;

namespace DSCC.CW1_MVC._11193.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Display(Name = "Department")]
        public string Name { get; set; }
    }
}
