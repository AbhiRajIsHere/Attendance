using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeePhoneNumberController : ControllerBase
    {
        private readonly GenericService<EmployeePhoneNumber> _genericService;

        public EmployeePhoneNumberController(GenericService<EmployeePhoneNumber> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhoneNumbers()
        {
            var phoneNumbers = await _genericService.GetAllAsync();
            return Ok(phoneNumbers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneNumberById(Guid id)
        {
            var phoneNumber = await _genericService.GetByIdAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            return Ok(phoneNumber);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoneNumber([FromBody] EmployeePhoneNumber phoneNumber)
        {
            await _genericService.AddAsync(phoneNumber);
            return CreatedAtAction(nameof(GetPhoneNumberById), new { id = phoneNumber.phone_id }, phoneNumber);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoneNumber(Guid id, [FromBody] EmployeePhoneNumber updatedPhoneNumber)
        {
            var phoneNumber = await _genericService.GetByIdAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            updatedPhoneNumber.phone_id = id;
            await _genericService.UpdateAsync(updatedPhoneNumber);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoneNumber(Guid id)
        {
            var phoneNumber = await _genericService.GetByIdAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(phoneNumber);
            return NoContent();
        }
    }
}
