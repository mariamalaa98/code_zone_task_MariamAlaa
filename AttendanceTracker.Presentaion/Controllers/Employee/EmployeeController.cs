using AttendanceTracker.Business.DTO;
using AttendanceTracker.Business.Iservice;
using AttendanceTracker.Bussiness.IServices;
using AttendanceTracker.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttendanceTracker.API.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeesController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var employeeSummaries = await _employeeService.GetEmployeeSummaryAsync();
            return View(employeeSummaries);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["FormMode"] = "Create"; // ✅ Set form mode
            await PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            ViewData["FormMode"] = "Create"; // ✅ Ensure mode for redisplay on validation error

            if (ModelState.IsValid)
            {
                var result = await _employeeService.CreateEmployeeAsync(employee);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            await PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            ViewData["FormMode"] = "Edit"; // ✅ Set form mode
            await PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Code)
                return BadRequest();

            ViewData["FormMode"] = "Edit"; // ✅ Ensure mode for redisplay on validation error

            ModelState.Remove("EmployeeCode");

            if (ModelState.IsValid)
            {
                var result = await _employeeService.UpdateEmployeeAsync(employee);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            await PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            ViewData["FormMode"] = "Delete"; // ✅ Set form mode
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDepartmentsDropDownList(object? selectedDepartment = null)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "DepartmentName", selectedDepartment);
        }
    }
}
