using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Business.DTO;
using AttendanceTracker.Business.Iservice;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Enums;
using AttendanceTracker.Data.Interface;


namespace AttendanceTracker.Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDepartment _departmentRepository;
        private readonly IEmployee _employeeRepository;
       //private readonly IAttendance _attendanceRepository;

        public EmployeeService(IEmployee employeeRepository, IDepartment departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeSummaryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var summaries = employees.Select(e =>
            {
                var monthAttendance = e.Attendances
                    .Where(a => a.Date.Date >= firstDayOfMonth && a.Date.Date <= lastDayOfMonth)
                    .ToList();

                var presentDays = monthAttendance.Count(a => a.Status == AttendanceStatus.Present);
                var absentDays = monthAttendance.Count(a => a.Status == AttendanceStatus.Absent);
                var totalDays = presentDays + absentDays;

                var attendancePercentage = totalDays == 0 ? 0 : (double)presentDays / totalDays * 100;

                return new EmployeeDTO
                {
                    EmployeeCode = e.Code,
                    FullName = e.FullName,
                    Email = e.Email,
                    DepartmentName = e.Department?.Name ?? "N/A",
                    PresentDays = presentDays,
                    AbsentDays = absentDays,
                    AttendancePercentage = Math.Round(attendancePercentage, 2)
                };
            }).ToList();

            return summaries;
        
        }
        public async Task<(bool Success, string ErrorMessage)> CreateEmployeeAsync(Employee employee)
        {
            var validationResult = await ValidateEmployee(employee);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            await _employeeRepository.AddEmployeeAsync(employee);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteEmployeeAsync(int employeeCode)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeCode);
            if (employee == null)
            {
                return (false, "Employee not found.");
            }

            await _employeeRepository.DeleteEmployeeAsync(employee);
            return (true, string.Empty);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeCode)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(employeeCode);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateEmployeeAsync(Employee employee)
        {
            var validationResult = await ValidateEmployee(employee, isUpdate: true);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return (true, string.Empty);
        }

        private async Task<(bool Success, string ErrorMessage)> ValidateEmployee(Employee employee, bool isUpdate = false)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            if (department == null)
            {
                return (false, "The selected department does not exist. Please refresh the page.");
            }
            int? employeeCodeToExclude = isUpdate ? employee.Code : null;
            if (await _employeeRepository.EmailExistsAsync(employee.Email, employeeCodeToExclude))
            {
                return (false, "An employee with this email already exists.");
            }

            return (true, string.Empty);
        }

       
    }
}