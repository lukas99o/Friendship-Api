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
                new() { Title = "Cooking & Travel Night", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1504674900247-0877df9cc836?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 3, 15, 18, 0, 0), EndTime = new DateTime(2026, 3, 15, 21, 0, 0) }, // matlagning
                new() { Title = "Photography Workshop", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 4, 10, 14, 0, 0), EndTime = new DateTime(2026, 4, 10, 17, 0, 0) }, // kamera
                new() { Title = "Fitness Bootcamp", IsPublic = true, CreatedByUserId = UserId, Img = "https://plus.unsplash.com/premium_photo-1726086677610-56498e65eed4?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", StartTime = new DateTime(2026, 5, 5, 9, 0, 0), EndTime = new DateTime(2026, 5, 5, 11, 0, 0) }, // träning
                new() { Title = "Morning Run", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1508609349937-5ec4ae374ebf?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 6, 1, 6, 30, 0), EndTime = new DateTime(2026, 6, 1, 7, 30, 0) }, // löpning
                new() { Title = "Hiking Adventure", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1500534623283-312aade485b7?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 7, 20, 8, 0, 0), EndTime = new DateTime(2026, 7, 20, 13, 0, 0) }, // vandring
                new() { Title = "Book Club", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1512820790803-83ca734da794?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 2, 25, 18, 0, 0), EndTime = new DateTime(2026, 2, 25, 20, 0, 0) }, // böcker
                new() { Title = "Hiking Trip", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1470770903676-69b98201ea1c?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 8, 5, 7, 0, 0), EndTime = new DateTime(2026, 8, 5, 12, 0, 0) }, // vandring
                new() { Title = "Wine Tasting", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1631118473337-dc07ccf892d2?q=80&w=1374&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", StartTime = new DateTime(2026, 9, 10, 19, 0, 0), EndTime = new DateTime(2026, 9, 10, 22, 0, 0) }, // vinprovning
                new() { Title = "Sushi Workshop", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 10, 12, 16, 0, 0), EndTime = new DateTime(2026, 10, 12, 19, 0, 0) }, // sushi
                new() { Title = "Beach Cleanup", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1644579156448-fd9346f7a333?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHN0cmFuZHN0JUMzJUE0ZG5pbmd8ZW58MHx8MHx8fDA%3D", StartTime = new DateTime(2026, 6, 15, 9, 0, 0), EndTime = new DateTime(2026, 6, 15, 12, 0, 0) }, // strandstädning
                new() { Title = "Stand-up Comedy", IsPublic = true, CreatedByUserId = UserId, Img = "https://plus.unsplash.com/premium_photo-1705883064233-e56b05f42b07?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Y29tZWR5fGVufDB8fDB8fHww", StartTime = new DateTime(2026, 11, 5, 20, 0, 0), EndTime = new DateTime(2026, 11, 5, 22, 0, 0) }, // komedi
                new() { Title = "Coffee Meetup", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 1, 20, 14, 0, 0), EndTime = new DateTime(2026, 1, 20, 16, 0, 0) }, // kaffe
                new() { Title = "Art & Chill", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1494526585095-c41746248156?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 3, 10, 18, 0, 0), EndTime = new DateTime(2026, 3, 10, 20, 0, 0) }, // konst
                new() { Title = "Outdoor Yoga", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 5, 20, 7, 0, 0), EndTime = new DateTime(2026, 5, 20, 8, 0, 0) }, // yoga utomhus
                new() { Title = "Language Exchange", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=800&q=80", StartTime = new DateTime(2026, 7, 1, 17, 0, 0), EndTime = new DateTime(2026, 7, 1, 19, 0, 0) }, // språk
                new() { Title = "Bike Ride", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1688136406066-4a7964d3db30?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGN5a2xhfGVufDB8fDB8fHww", StartTime = new DateTime(2026, 8, 15, 9, 0, 0), EndTime = new DateTime(2026, 8, 15, 11, 0, 0) }, // cykling
                new() { Title = "Movie Marathon", IsPublic = true, CreatedByUserId = UserId, Img = "https://plus.unsplash.com/premium_photo-1661675440353-6a6019c95bc7?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8bW92aWV8ZW58MHx8MHx8fDA%3D", StartTime = new DateTime(2026, 9, 25, 15, 0, 0), EndTime = new DateTime(2026, 9, 25, 20, 0, 0) }, // filmkväll
                new() { Title = "Street Food Tour", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1603088549155-6ae9395b928f?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHN0cmVldCUyMGZvb2R8ZW58MHx8MHx8fDA%3D", StartTime = new DateTime(2026, 10, 5, 13, 0, 0), EndTime = new DateTime(2026, 10, 5, 16, 0, 0) }, // street food
                new() { Title = "Photography Walk", IsPublic = true, CreatedByUserId = UserId, Img = "https://plus.unsplash.com/premium_photo-1664301085009-470198636a73?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Zm90b2dyYWZpfGVufDB8fDB8fHww", StartTime = new DateTime(2026, 11, 15, 10, 0, 0), EndTime = new DateTime(2026, 11, 15, 12, 0, 0) }, // foto promenad
                new() { Title = "Midnight Picnic", IsPublic = true, CreatedByUserId = UserId, Img = "https://images.unsplash.com/photo-1626450780751-d0d764738e86?w=400&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8cGljbmljJTIwbmlnaHR8ZW58MHx8MHx8fDA%3D", StartTime = new DateTime(2026, 12, 20, 22, 0, 0), EndTime = new DateTime(2026, 12, 21, 1, 0, 0) }, // nattpicknick
            };

            await _context.AddRangeAsync(events);
            await _context.SaveChangesAsync();

            // EventId måste sättas efter att events har sparats och fått sina riktiga ID:n
            var eventParticipants = events.Select(e => new EventParticipant
            {
                UserId = UserId,
                EventId = e.Id,
                Role = "Host",
                JoinedAt = DateTime.UtcNow
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
