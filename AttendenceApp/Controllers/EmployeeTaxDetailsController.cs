using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendenceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTaxDetailsController : ControllerBase
    {
        private readonly GenericService<EmployeeTaxDetail> _service;

        public EmployeeTaxDetailsController(GenericService<EmployeeTaxDetail> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            (await _service.GetByIdAsync(id)) is EmployeeTaxDetail taxDetail ? Ok(taxDetail) : NotFound();

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeTaxDetail taxDetail)
        {
            await _service.AddAsync(taxDetail);
            return CreatedAtAction(nameof(GetById), new { id = taxDetail.tax_id }, taxDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EmployeeTaxDetail taxDetail)
        {
            if (id != taxDetail.tax_id) return BadRequest();
            await _service.UpdateAsync(taxDetail);
            return NoContent();
        }

       
    }
}
