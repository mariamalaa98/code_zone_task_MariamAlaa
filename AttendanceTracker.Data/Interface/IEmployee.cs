using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Entity;

namespace AttendanceTracker.Data.Interface
{
    public interface IEmployee
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeCode);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        Task<bool> EmailExistsAsync(string email, int? excludeEmployeeCode = null);

    }
}
