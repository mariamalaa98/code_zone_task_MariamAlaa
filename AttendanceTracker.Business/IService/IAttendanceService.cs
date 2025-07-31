using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Enums;

namespace AttendanceTracker.Business.IService
{
   public interface IAttendanceService
    {
        Task<Attendance?> GetAttendanceStatusAsync(int employeeId, DateTime date);
        Task<(bool Success, string ErrorMessage)> RecordAttendanceAsync(int employeeId, DateTime date, AttendanceStatus status);
        Task<(bool Success, string ErrorMessage)> DeleteAttendanceAsync(int employeeId, DateTime date);
        Task<IEnumerable<Attendance>> GetFilteredAttendanceAsync(int? departmentId, int? employeeId, DateTime? startDate, DateTime? endDate);
    }
}
