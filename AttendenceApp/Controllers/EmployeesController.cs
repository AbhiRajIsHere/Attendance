using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;
namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly GenericService<Employee> _genericService;

        public EmployeesController(GenericService<Employee> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _genericService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _genericService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _genericService.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employee_id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] Employee updatedEmployee)
        {
            var employee = await _genericService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            updatedEmployee.employee_id = id;
            await _genericService.UpdateAsync(updatedEmployee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _genericService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(employee);
            return NoContent();
        }
    }
}
