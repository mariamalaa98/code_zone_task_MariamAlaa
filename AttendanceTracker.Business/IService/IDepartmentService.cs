using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Business.DTO;
using AttendanceTracker.Bussiness.DTO;
using AttendanceTracker.Data.Entity;

namespace AttendanceTracker.Bussiness.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task<IEnumerable<DepartmentDropListDTO>> GetForDropdownAsync();


        Task DeleteDepartmentAsync(int id);



    }
}
