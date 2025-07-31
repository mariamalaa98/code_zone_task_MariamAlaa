using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Business.DTO;
using AttendanceTracker.Data.Entity;

namespace AttendanceTracker.Business.Iservice
{
   public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeeSummaryAsync();
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int employeeCode);
        Task<(bool Success, string ErrorMessage)> CreateEmployeeAsync(Employee employee);
        Task<(bool Success, string ErrorMessage)> UpdateEmployeeAsync(Employee employee);
        Task<(bool Success, string ErrorMessage)> DeleteEmployeeAsync(int employeeCode);
    }
}
