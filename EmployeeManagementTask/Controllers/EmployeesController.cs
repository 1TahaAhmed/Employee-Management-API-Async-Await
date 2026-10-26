using EmployeeManagementAPI.Services;
using EmployeeManagementTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll(
        CancellationToken cancellationToken)
    {
        var employees = await _employeeService.GetAllAsync(
            cancellationToken);

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var employee = await _employeeService.GetByIdAsync(
            id,
            cancellationToken);

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(
        Employee employee,
        CancellationToken cancellationToken)
    {
        var createdEmployee = await _employeeService.CreateAsync(
            employee,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdEmployee.Id },
            createdEmployee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        Employee employee,
        CancellationToken cancellationToken)
    {
        var updated = await _employeeService.UpdateAsync(
            id,
            employee,
            cancellationToken);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _employeeService.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}