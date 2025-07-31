
using AttendanceTracker.Business.IService;
using AttendanceTracker.Business.Service;
using AttendanceTracker.Data.Context;
using AttendanceTracker.Data.Interface;
using AttendanceTracker.Data.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EmployeeAttendanceDB"));
builder.Services.AddControllersWithViews();

// Corrected service registrations
builder.Services.AddScoped<AttendanceTracker.Data.Interface.IDepartment, AttendanceTracker.Data.Repository.DepartmentRepo>();
builder.Services.AddScoped<AttendanceTracker.Bussiness.IServices.IDepartmentService, AttendanceTracker.Bussiness.Services.DepartmentService>();
builder.Services.AddScoped<AttendanceTracker.Data.Interface.IEmployee, AttendanceTracker.Data.Repository.EmployeeRepo>();
builder.Services.AddScoped<AttendanceTracker.Business.Iservice.IEmployeeService, AttendanceTracker.Business.Service.EmployeeService>();
builder.Services.AddScoped<AttendanceTracker.Data.Interface.IAttendance, AttendanceTracker.Data.Repository.AttendanceRepo>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
// Replace this line if it exists (incorrect namespace):
// using AttendanceTracker.Data.Interfaces;

// With the correct namespace:

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
