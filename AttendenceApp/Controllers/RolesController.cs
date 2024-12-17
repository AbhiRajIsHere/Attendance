using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly GenericService<Role> _genericService;

        public RolesController(GenericService<Role> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _genericService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _genericService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            await _genericService.AddAsync(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.role_id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] Role updatedRole)
        {
            var role = await _genericService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            updatedRole.role_id = id;
            await _genericService.UpdateAsync(updatedRole);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _genericService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(role);
            return NoContent();
        }
    }
}
