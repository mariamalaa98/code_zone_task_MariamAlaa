using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Enums;

namespace AttendanceTracker.Data.Interface
{
    public interface IAttendance
    {
        Task<IEnumerable<Attendance>> GetFilteredAsync(int? departmentId, int? employeeId, DateTime? startDate, DateTime? endDate);
        Task<Attendance?> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(Attendance attendance);

    }
}
