using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AttendanceTracker.Data.Entity
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public int Code { get; set; }

        [Required]
        [RegularExpression(@"^([A-Za-z]{2,}\s){3}[A-Za-z]{2,}$", ErrorMessage = "Full name must be four names, each at least 2 letters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        // Navigation
        public Department Department { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
