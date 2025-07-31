using AttendanceTracker.Business.Iservice;
using AttendanceTracker.Business.IService;
using AttendanceTracker.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttendanceTracker.Presentaion.Controllers
{
    public class AttendancesController : Controller
    {
        
            private readonly IAttendanceService _attendanceService;
            private readonly IEmployeeService _employeeService;

            public AttendancesController(IAttendanceService attendanceService, IEmployeeService employeeService)
            {
                _attendanceService = attendanceService;
                _employeeService = employeeService;
            }

            public async Task<IActionResult> Index()
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                ViewBag.Employees = new SelectList(employees, "EmployeeCode", "FullName");
                return View();
            }

            [HttpGet]
            public async Task<IActionResult> GetAttendanceStatus(int employeeId, DateTime date)
            {
                if (date.Date > DateTime.Today)
                {
                    return Json(new { status = "Future Date" });
                }

                var attendance = await _attendanceService.GetAttendanceStatusAsync(employeeId, date);
                if (attendance == null)
                {
                    return Json(new { status = "Not Marked" });
                }

                // Use enum value directly
                return Json(new { status = attendance.Status.ToString() });
            }

            [HttpPost]
            public async Task<IActionResult> RecordAttendance(int employeeId, DateTime date, AttendanceStatus status)
            {
                var result = await _attendanceService.RecordAttendanceAsync(employeeId, date, status);
                if (!result.Success)
                {
                    return Json(new { success = false, message = result.ErrorMessage });
                }

                return Json(new { success = true });
            }

            [HttpPost]
            public async Task<IActionResult> DeleteAttendance(int employeeId, DateTime date)
            {
                var result = await _attendanceService.DeleteAttendanceAsync(employeeId, date);
                if (!result.Success)
                {
                    return Json(new { success = false, message = result.ErrorMessage });
                }

                return Json(new { success = true });
            }
        }
    }