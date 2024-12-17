using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserRolesController : ControllerBase
    {
        private readonly GenericService<UserRole> _genericService;

        public UserRolesController(GenericService<UserRole> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var userRoles = await _genericService.GetAllAsync();
            return Ok(userRoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRoleById(Guid id)
        {
            var userRole = await _genericService.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole([FromBody] UserRole userRole)
        {
            await _genericService.AddAsync(userRole);
            return CreatedAtAction(nameof(GetUserRoleById), new { id = userRole.user_role_id }, userRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UserRole updatedUserRole)
        {
            var userRole = await _genericService.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            updatedUserRole.user_role_id = id;
            await _genericService.UpdateAsync(updatedUserRole);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(Guid id)
        {
            var userRole = await _genericService.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(userRole);
            return NoContent();
        }
    }
}
