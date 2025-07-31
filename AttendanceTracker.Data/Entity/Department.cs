using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceTracker.Data.Entity
{
    public class Department
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{4}$", ErrorMessage = "Code must be exactly 4 uppercase letters.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        // Navigation
        public ICollection<Employee> Employees { get; set; }

    }
}
