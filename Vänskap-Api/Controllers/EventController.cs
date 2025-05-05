using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Vänskap_Api.Models.Dtos.Event;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDto createEvent)
        {
            var evnt = await _eventService.CreateEvent(createEvent);

            if (evnt == null)
            {
                return BadRequest("Could not create event");
            }

            return CreatedAtAction(nameof(ReadEvent), new { id = evnt.EventId }, evnt);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadEventDto>>> ReadAllEvents()
        {
            return Ok(await _eventService.ReadAllEvents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEventDto>> ReadEvent(int id)
        {
            var evnt = await _eventService.ReadEvent(id);
            if (evnt == null) return NotFound();
            return Ok(evnt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto updateEvent)
        {
            var success = await _eventService.UpdateEvent(id, updateEvent);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _eventService.DeleteEvent(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
