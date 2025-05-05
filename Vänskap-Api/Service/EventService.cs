using Microsoft.EntityFrameworkCore;
using Sprache;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.Event;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Service
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReadEventDto> CreateEvent(EventDto createEvent)
        {
            var interests = new List<Interest>();

            if (createEvent.Interests != null)
            {
                interests = await _context.Interests
                    .Where(i => createEvent.Interests.Contains(i.Name))
                    .ToListAsync();
            }

            if (createEvent.StartTime == null) createEvent.StartTime = DateTime.Now;
            if (createEvent.EndTime == null) createEvent.EndTime = DateTime.Now.AddDays(1);

            var createObj = new Event()
            {
                Title = createEvent.Title,
                Description = createEvent.Description,
                StartTime = createEvent.StartTime,
                CreatedByUserId = createEvent.UserId,
                EndTime = createEvent.EndTime,
                AgeRangeMax = createEvent.AgeRangeMax,
                AgeRangeMin = createEvent.AgeRangeMin,
                IsPublic = createEvent.IsPublic,
                Interests = interests,
                Location = createEvent.Location
            };

            await _context.Events.AddAsync(createObj);
            await _context.SaveChangesAsync();

            var eventParticipant = new EventParticipant()
            {
                Role = "Host",
                UserName = "Admin",
                UserId = createEvent.UserId,
                EventId = createObj.Id
            };

            createObj.EventParticipants.Add(eventParticipant);
            _context.Events.Update(createObj);
            await _context.EventParticipants.AddAsync(eventParticipant);
            await _context.SaveChangesAsync();

            var eventParticiantList = new List<EventParticipantDto>();
            var eventParticipantDto = new EventParticipantDto()
            {
                UserName = eventParticipant.UserName,
                Role = eventParticipant.Role,
            };
            eventParticiantList.Add(eventParticipantDto);

            var evnt = new ReadEventDto()
            {
                EventId = createObj.Id,
                UserId = createObj.CreatedByUserId,
                Title = createObj.Title,
                Description = createObj.Description,
                StartTime = createObj.StartTime,
                EndTime = createObj.EndTime,
                Location = createObj.Location,
                AgeRangeMax = createObj.AgeRangeMax,
                AgeRangeMin = createObj.AgeRangeMin,
                Interests = interests.Select(i => i.Name).ToList(),
                EventParticipants = eventParticiantList,
                IsPublic = createObj.IsPublic
            };

            return evnt;
        }

        public async Task<IEnumerable<ReadEventDto>> ReadAllEvents()
        {
            var result = await _context.Events.Include(i => i.Interests).Include(e => e.EventParticipants).ToListAsync();

            var eventList = result.Select(r => new ReadEventDto
            {
                EventId = r.Id,
                UserId = r.CreatedByUserId,
                Title = r.Title,
                Description = r.Description,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Location = r.Location,
                AgeRangeMax = r.AgeRangeMax,
                AgeRangeMin = r.AgeRangeMin,
                Interests = r.Interests?.Select(i => i.Name).ToList(),
                EventParticipants = r.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = p.UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = r.IsPublic
            }).ToList();

            return eventList;
        }

        public async Task<ReadEventDto?> ReadEvent(int id)
        {
            var result = await _context.Events
                .Include(e => e.Interests)
                .Include(e => e.EventParticipants)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result == null) return null;

            var evnt = new ReadEventDto()
            {
                EventId = result.Id,
                UserId = result.CreatedByUserId,
                Title = result.Title,
                Description = result.Description,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                Location = result.Location,
                AgeRangeMax = result.AgeRangeMax,
                AgeRangeMin = result.AgeRangeMin,
                Interests = result.Interests?.Select(i => i.Name).ToList(),
                EventParticipants = result.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = p.UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = result.IsPublic
            };

            return evnt;
        }

        public async Task<bool> UpdateEvent(int id, EventDto updateEvent)
        {
            var evnt = await _context.Events
                .Include(e => e.Interests)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evnt == null) return false;
            var currentinterests = new List<Interest>();

            if (updateEvent.Interests != null)
            {
                currentinterests = await _context.Interests
                    .Where(i => updateEvent.Interests.Contains(i.Name))
                    .ToListAsync();
            }

            evnt.CreatedByUserId = updateEvent.UserId;
            evnt.Title = updateEvent.Title;
            evnt.Description = updateEvent.Description;
            evnt.Location = updateEvent.Location;
            evnt.AgeRangeMax = updateEvent.AgeRangeMax;
            evnt.AgeRangeMin = updateEvent.AgeRangeMin;
            evnt.IsPublic = updateEvent.IsPublic;
            evnt.Interests = currentinterests;
            if (updateEvent.StartTime != null) evnt.StartTime = updateEvent.StartTime;
            if (updateEvent.EndTime != null) evnt.EndTime = updateEvent.EndTime;

            _context.Update(evnt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            var deleteEvent = await _context.Events.SingleOrDefaultAsync(e => e.Id == id);

            if (deleteEvent != null)
            {
                var eventParticipants = deleteEvent.EventParticipants;
                _context.EventParticipants.RemoveRange(eventParticipants);
                _context.Events.Remove(deleteEvent);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
