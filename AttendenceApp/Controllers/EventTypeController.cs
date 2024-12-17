using Microsoft.AspNetCore.Mvc;
using AttendenceApp.Models;
using AttendenceApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace AttendenceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventTypeController : ControllerBase
    {
        private readonly GenericService<EventType> _genericService;

        public EventTypeController(GenericService<EventType> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventTypes()
        {
            var eventTypes = await _genericService.GetAllAsync();
            return Ok(eventTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventTypeById(Guid id)
        {
            var eventType = await _genericService.GetByIdAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventType([FromBody] EventType eventType)
        {
            await _genericService.AddAsync(eventType);
            return CreatedAtAction(nameof(GetEventTypeById), new { id = eventType.event_type_id }, eventType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventType(Guid id, [FromBody] EventType updatedEventType)
        {
            var eventType = await _genericService.GetByIdAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }
            updatedEventType.event_type_id = id;
            await _genericService.UpdateAsync(updatedEventType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventType(Guid id)
        {
            var eventType = await _genericService.GetByIdAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }
            await _genericService.DeleteAsync(eventType);
            return NoContent();
        }
    }
}
