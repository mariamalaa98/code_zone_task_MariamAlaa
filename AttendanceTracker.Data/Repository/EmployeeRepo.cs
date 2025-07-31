using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Interface;
using AttendanceTracker.Data.Context;
using AttendanceTracker.Data.Entity;
using Microsoft.EntityFrameworkCore;


namespace AttendanceTracker.Data.Repository
{
    public class EmployeeRepo : IEmployee
    {
       

        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeEmployeeCode = null)
        {
            var query = _context.Employees.AsQueryable();

            if (excludeEmployeeCode.HasValue)
            {
                query = query.Where(e => e.Code != excludeEmployeeCode.Value);
            }

            return await query.AnyAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Attendances)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeCode)
        {
            return await _context.Employees.FindAsync(employeeCode);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
