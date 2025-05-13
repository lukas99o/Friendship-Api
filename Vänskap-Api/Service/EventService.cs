using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sprache;
using System.Security.Claims;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.Event;
using Vänskap_Api.Service.IService;

namespace Vänskap_Api.Service
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string UserId => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException(nameof(UserId));
        private string UserName => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException(nameof(UserName)); 

        public EventService(ApplicationDbContext context, IHttpContextAccessor contextAssessor)
        {
            _context = context;
            _contextAccessor = contextAssessor;
        }

        public async Task<ReadEventDto> CreateEvent(EventDto createEvent)
        {
            var user = await _context.Users.FindAsync(UserId);
            var interests = new List<Interest>();

            if (createEvent.Interests != null)
            {
                interests = await _context.Interests
                    .Where(i => createEvent.Interests.Contains(i.Name))
                    .ToListAsync();
            }

            var createObj = new Event()
            {
                Title = createEvent.Title,
                Description = createEvent.Description,
                StartTime = createEvent.StartTime,
                CreatedByUserId = UserId,
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
                UserId = UserId,
                EventId = createObj.Id
            };

            createObj.EventParticipants.Add(eventParticipant);
            _context.Events.Update(createObj);
            await _context.EventParticipants.AddAsync(eventParticipant);
            user?.CreatedEvents.Add(createObj);
            await _context.SaveChangesAsync();

            var eventParticiantList = new List<EventParticipantDto>();
            var eventParticipantDto = new EventParticipantDto()
            {
                UserName = UserName,
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

        public async Task<IEnumerable<ReadEventDto>> ReadAllPublicEvents(List<string?> interests, int ageMin, int ageMax)
        {
            var result = await _context.Events.Include(i => i.Interests).Include(e => e.EventParticipants).Where(e => e.IsPublic == true).ToListAsync();
            
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
                    UserName = UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = r.IsPublic
            }).ToList();

            return eventList;
        }

        public async Task<IEnumerable<ReadEventDto>> GetAllFriendEvents()
        {
            var friendIds = await _context.Friendships
                .Where(f => f.UserId == UserId)
                .Select(f => f.FriendId)
                .ToListAsync();

            var events = await _context.Events
                .Where(e => friendIds.Contains(e.CreatedByUserId))
                .ToListAsync();

            var eventList = events.Select(e => new ReadEventDto
            {
                EventId = e.Id,
                UserId = e.CreatedByUserId,
                Title = e.Title,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Location = e.Location,
                AgeRangeMax = e.AgeRangeMax,
                AgeRangeMin = e.AgeRangeMin,
                Interests = e.Interests?.Select(i => i.Name).ToList(),
                EventParticipants = e.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = e.IsPublic
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

            if (result.IsPublic)
            {
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
                        UserName = UserName,
                        Role = p.Role,
                    }).ToList(),
                    IsPublic = result.IsPublic
                };

                return evnt;
            }
            else
            {
                var eventOwnerId = result.CreatedByUserId;
                var isFriend = await _context.Friendships.AnyAsync(f => 
                    (f.UserId == UserId && f.FriendId == eventOwnerId) ||
                    (f.FriendId == UserId && f.UserId == eventOwnerId)
                );

                if (!isFriend) return null;

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
                        UserName = UserName,
                        Role = p.Role,
                    }).ToList(),
                    IsPublic = result.IsPublic
                };

                return evnt;
            }
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

            evnt.CreatedByUserId = UserId;
            evnt.Title = updateEvent.Title;
            evnt.Description = updateEvent.Description;
            evnt.Location = updateEvent.Location;
            evnt.AgeRangeMax = updateEvent.AgeRangeMax;
            evnt.AgeRangeMin = updateEvent.AgeRangeMin;
            evnt.IsPublic = updateEvent.IsPublic;
            evnt.Interests = currentinterests;
            evnt.StartTime = updateEvent.StartTime;
            evnt.EndTime = updateEvent.EndTime;

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
