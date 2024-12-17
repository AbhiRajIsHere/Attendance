using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeAadharDetailsController : ControllerBase
    {
        private readonly GenericService<employeeaadhardetails> _genericService;

        public EmployeeAadharDetailsController(GenericService<employeeaadhardetails> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAadharDetails()
        {
            var details = await _genericService.GetAllAsync();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAadharDetailById(Guid id)
        {
            var detail = await _genericService.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> AddAadharDetail([FromBody] employeeaadhardetails detail)
        {
            await _genericService.AddAsync(detail);
            return CreatedAtAction(nameof(GetAadharDetailById), new { id = detail.aadhar_id }, detail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAadharDetail(Guid id, [FromBody] employeeaadhardetails updatedDetail)
        {
            var detail = await _genericService.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            updatedDetail.aadhar_id = id;
            await _genericService.UpdateAsync(updatedDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAadharDetail(Guid id)
        {
            var detail = await _genericService.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(detail);
            return NoContent();
        }
    }
}
