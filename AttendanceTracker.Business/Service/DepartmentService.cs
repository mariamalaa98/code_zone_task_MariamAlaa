using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Bussiness.IServices;
using AttendanceTracker.Bussiness.DTO;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Interface;
using AttendanceTracker.Business.DTO;


namespace AttendanceTracker.Bussiness.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartment _department;
        public DepartmentService(IDepartment department)
        {
            _department = department;
        }
        public async Task AddDepartmentAsync(Department department)
        {
            if (!await _department.IsNameUniqueAsync(department.Name))
            {
                throw new ArgumentException("Department name must be unique.");
            }
            if (!await _department.IsCodeUniqueAsync(department.Code))
            {
                throw new ArgumentException("Department code must be unique.");
            }
            await _department.AddAsync(department);
            await _department.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _department.GetByIdAsync(id);
            if (department == null)
            {
                throw new KeyNotFoundException("Department not found.");
            }
            await _department.DeleteAsync(id);
            await _department.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _department.GetAllAsync();

            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Location = d.Location,
                EmployeeCount = d.Employees?.Count ?? 0
            });

        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var isDepartment = await _department.GetByIdAsync(id);
            if (isDepartment == null)
            {
                throw new KeyNotFoundException("Department not found.");
            }
            return isDepartment;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _department.UpdateAsync(department);
            await _department.SaveChangesAsync();
        }
        public async Task<IEnumerable<DepartmentDropListDTO>> GetForDropdownAsync()
        {
            var departments = await _department.GetForDropdownAsync();

            return departments.Select(d => new DepartmentDropListDTO
            {
                Id = d.Id,
                Name = d.Name

            });
        }




    }
}
