using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Interface;
namespace AttendanceTracker.Data.Repository;

using AttendanceTracker.Data.Entity;
using AttendanceTracker.Data.Interface;
using AttendanceTracker.Data.Context;

using Microsoft.EntityFrameworkCore;


public class DepartmentRepo :IDepartment
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Department department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        _context.Departments.Add(department);
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
            throw new KeyNotFoundException($"Department with ID {id} not found.");

        _context.Departments.Remove(department);
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments
                     .Include(d => d.Employees)
                     .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<bool> IsNameUniqueAsync(string name, int? excludeId = null)
    {
        return !await _context.Departments
            .AnyAsync(d => d.Name == name && (excludeId == null || d.Id != excludeId));
    }

    public async Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null)
    {
        return !await _context.Departments
            .AnyAsync(d => d.Code == code && (excludeId == null || d.Id != excludeId));
    }

    public async Task<IEnumerable<Department>> GetForDropdownAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        var existing = await _context.Departments.FindAsync(department.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Department with ID {department.Id} not found.");

        existing.Name = department.Name;
        existing.Code = department.Code;
        existing.Location = department.Location;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}



