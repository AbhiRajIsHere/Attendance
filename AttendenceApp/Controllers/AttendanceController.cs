using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly GenericService<attendance> _genericService;

        public AttendanceController(GenericService<attendance> genericService)
        {
            _genericService = genericService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            var attendance = await _genericService.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(attendance);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attendances = await _genericService.GetAllAsync();
            return Ok(attendances);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance([FromBody] attendance attendance)
        {
            // Ensure event_date and created_at are in UTC
            attendance.event_date = DateTime.SpecifyKind(attendance.event_date, DateTimeKind.Utc);
            attendance.created_at = DateTime.UtcNow;

            await _genericService.AddAsync(attendance);
            return CreatedAtAction(nameof(GetAllAttendance), new { id = attendance.attendance_id }, attendance);
        }
    }
}
