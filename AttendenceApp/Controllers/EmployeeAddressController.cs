using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeAddressController : ControllerBase
    {
        private readonly GenericService<EmployeeAddress> _genericService;

        public EmployeeAddressController(GenericService<EmployeeAddress> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _genericService.GetAllAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var address = await _genericService.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] EmployeeAddress address)
        {
            await _genericService.AddAsync(address);
            return CreatedAtAction(nameof(GetAddressById), new { id = address.address_id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] EmployeeAddress updatedAddress)
        {
            var address = await _genericService.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            updatedAddress.address_id = id;
            await _genericService.UpdateAsync(updatedAddress);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _genericService.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(address);
            return NoContent();
        }
    }
}
