using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Business.IService;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Enums;
using AttendanceTracker.Data.Interface;

namespace AttendanceTracker.Business.Service
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IAttendance _attendanceRepository;

        public AttendanceService(IAttendance attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Attendance?> GetAttendanceStatusAsync(int employeeId, DateTime date)
        {
            return await _attendanceRepository.GetByEmployeeAndDateAsync(employeeId, date);
        }

        public async Task<(bool Success, string ErrorMessage)> RecordAttendanceAsync(int employeeId, DateTime date, AttendanceStatus status)
        {
            if (date.Date > DateTime.Today)
            {
                return (false, "Cannot record attendance for a future date.");
            }

            var existingAttendance = await _attendanceRepository.GetByEmployeeAndDateAsync(employeeId, date);

            if (existingAttendance != null)
            {
                existingAttendance.Status = status;
                await _attendanceRepository.UpdateAsync(existingAttendance);
            }
            else
            {
                var newAttendance = new Attendance
                {
                    EmployeeId = employeeId,
                    Date = date.Date,
                    Status = status
                };
                await _attendanceRepository.AddAsync(newAttendance);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAttendanceAsync(int employeeId, DateTime date)
        {
            if (date.Date > DateTime.Today)
            {
                return (false, "Cannot modify attendance for a future date.");
            }

            var existingAttendance = await _attendanceRepository.GetByEmployeeAndDateAsync(employeeId, date);

            if (existingAttendance != null)
            {
                await _attendanceRepository.DeleteAsync(existingAttendance);
            }

            return (true, string.Empty);
        }

        public async Task<IEnumerable<Attendance>> GetFilteredAttendanceAsync(int? departmentId, int? employeeId, DateTime? startDate, DateTime? endDate)
        {
            return await _attendanceRepository.GetFilteredAsync(departmentId, employeeId, startDate, endDate);
        }
    }
}