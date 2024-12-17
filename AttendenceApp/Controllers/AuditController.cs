using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private readonly GenericService<audit> _genericService;

        public AuditController(GenericService<audit> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAudits()
        {
            var audits = await _genericService.GetAllAsync();
            return Ok(audits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditById(Guid id)
        {
            var audit = await _genericService.GetByIdAsync(id);
            if (audit == null)
            {
                return NotFound();
            }
            return Ok(audit);
        }

        [HttpPost]
        public async Task<IActionResult> AddAudit([FromBody] audit audit)
        {
            await _genericService.AddAsync(audit);
            return CreatedAtAction(nameof(GetAuditById), new { id = audit.audit_id }, audit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAudit(Guid id, [FromBody] audit updatedAudit)
        {
            var audit = await _genericService.GetByIdAsync(id);
            if (audit == null)
            {
                return NotFound();
            }
            updatedAudit.audit_id = id;
            await _genericService.UpdateAsync(updatedAudit);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudit(Guid id)
        {
            var audit = await _genericService.GetByIdAsync(id);
            if (audit == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(audit);
            return NoContent();
        }
    }
}
