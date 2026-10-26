using EmployeeManagementAPI.Data;
using EmployeeManagementTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken);
    }

    public async Task<Employee> CreateAsync(
        Employee employee,
        CancellationToken cancellationToken)
    {
        await _context.Employees.AddAsync(
            employee,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return employee;
    }

    public async Task<bool> UpdateAsync(
        int id,
        Employee employee,
        CancellationToken cancellationToken)
    {
        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken);

        if (existingEmployee is null)
            return false;

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.Salary = employee.Salary;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken);

        if (employee is null)
            return false;

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}