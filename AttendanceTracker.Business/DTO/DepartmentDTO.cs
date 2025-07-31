using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceTracker.Business.DTO
{
    public class DepartmentDTO
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{4}$")]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Location { get; set; }
        public int EmployeeCount { get; set; }

    }
}
