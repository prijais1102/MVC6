using System.ComponentModel.DataAnnotations;

namespace MVC6.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [RegularExpression("[a-z A-z]+")]
        public string Name { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        [Range(20000, 50000)]
        public int Salary { get; set; }

    }
}
