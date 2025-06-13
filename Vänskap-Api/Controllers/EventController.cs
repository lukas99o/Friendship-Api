using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.Event;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string UserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException(nameof(UserId));

        public EventController(IEventService eventService, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpPost("join/{id}")]
        public async Task<IActionResult> JoinEvent(int id)
        {
            var result = await _eventService.JoinEvent(id);
            if (!result) return BadRequest("Could not join event.");

            return Ok("Joined the event.");
        }

        [HttpPost("leave/{id}")]
        public async Task<IActionResult> LeaveEvent(int id)
        {
            var result = await _eventService.LeaveEvent(id);
            if (!result) return BadRequest("Could not leave event");

            return Ok("Left the event");
        }

        [HttpPost("publicevents")]
        public async Task<ActionResult<IEnumerable<ReadEventDto>>> ReadAllPublicEvents(ReadAllPublicEventsDto dto)
        {
            return Ok(await _eventService.ReadAllPublicEvents(dto.Interests!, dto.AgeMin, dto.AgeMax));
        }

        [HttpGet("friendsevents")]
        public async Task<ActionResult<IEnumerable<ReadEventDto>>> GetAllFriendEvents()
        {
            return Ok(await _eventService.GetAllFriendEvents());
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _eventService.DeleteEvent(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("interests")]
        public async Task<ActionResult<List<string>>> GetInterests()
        {
            return Ok(await _eventService.GetInterests());
        }

        [HttpGet("participant-status")]
        public async Task<ActionResult<List<int>>> GetParticipantStatus()
        {
            return Ok(await _eventService.EventPartcipantStatus());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("seed-eventdata")]
        public async Task<IActionResult> SeedEventData()
        {
            List<Event> events = new()
            {
                new() { Title = "Cooking & Travel Night", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Photography Workshop", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Fitness Bootcamp", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Morning Run", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Hiking Adventure", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Book Club", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Hiking Trip", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Wine Tasting", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Sushi Workshop", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Beach Cleanup", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Stand-up Comedy", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Coffee Meetup", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Art & Chill", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Outdoor Yoga", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Language Exchange", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Bike Ride", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Movie Marathon", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Street Food Tour", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Photography Walk", IsPublic = true, CreatedByUserId = UserId },
                new() { Title = "Midnight Picnic", IsPublic = true, CreatedByUserId = UserId }
            };

            await _context.AddRangeAsync(events);
            await _context.SaveChangesAsync();

            // EventId måste sättas efter att events har sparats och fått sina riktiga ID:n
            var eventParticipants = events.Select(e => new EventParticipant
            {
                UserId = UserId,
                EventId = e.Id,
                Role = "Host",
                JoinedAt = DateTime.Now
            }).ToList();

            var eventInterests = new List<EventInterest>
            {
                new() { InterestId = 1, EventId = events[0].Id },
                new() { InterestId = 2, EventId = events[1].Id },
                new() { InterestId = 3, EventId = events[1].Id },
                new() { InterestId = 4, EventId = events[2].Id },
                new() { InterestId = 5, EventId = events[3].Id },
                new() { InterestId = 6, EventId = events[4].Id }
            };

            await _context.AddRangeAsync(eventParticipants);
            await _context.AddRangeAsync(eventInterests);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
