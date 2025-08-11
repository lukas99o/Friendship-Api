using Microsoft.EntityFrameworkCore;
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
        private string UserName => _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? throw new ArgumentNullException(nameof(UserName)); 

        public EventService(ApplicationDbContext context, IHttpContextAccessor contextAssessor)
        {
            _context = context;
            _contextAccessor = contextAssessor;
        }

        public async Task<ReadEventDto?> CreateEvent(EventDto createEvent)
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
                Location = createEvent.Location,
                Img = createEvent.Img
            };

            await _context.Events.AddAsync(createObj);
            await _context.SaveChangesAsync();
            
            if (createObj != null)
            {
                var eventParticipant = new EventParticipant()
                {
                    Role = "Host",
                    UserId = UserId,
                    EventId = createObj.Id
                };

                foreach (var interest in interests)
                {
                    var eventInterest = new EventInterest()
                    {
                        InterestId = interest.Id,
                        EventId = createObj!.Id
                    };

                    createObj?.EventInterests?.Add(eventInterest);
                }

                createObj?.EventParticipants.Add(eventParticipant);
                _context.Events.Update(createObj!);
                await _context.EventParticipants.AddAsync(eventParticipant);
                user?.CreatedEvents.Add(createObj!);
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
                    EventId = createObj!.Id,
                    UserId = createObj.CreatedByUserId,
                    Title = createObj.Title,
                    Description = createObj.Description,
                    StartTime = createEvent.StartTime.Kind == DateTimeKind.Utc
                        ? createEvent.StartTime
                        : DateTime.SpecifyKind(createEvent.StartTime, DateTimeKind.Local).ToUniversalTime(),
                    EndTime = createEvent.EndTime.Kind == DateTimeKind.Utc
                        ? createEvent.EndTime
                        : DateTime.SpecifyKind(createEvent.EndTime, DateTimeKind.Local).ToUniversalTime(),
                    Location = createObj.Location,
                    AgeRangeMax = createObj.AgeRangeMax,
                    AgeRangeMin = createObj.AgeRangeMin,
                    Interests = interests.Select(i => i.Name).ToList(),
                    EventParticipants = eventParticiantList,
                    IsPublic = createObj.IsPublic,
                    Img = createObj.Img
                };

                return evnt;
            }

            return null;
        }

        public async Task<IEnumerable<ReadEventDto>> ReadAllPublicEvents(List<string?> interests, int? ageMin, int? ageMax)
        {
            var query = _context.Events
                .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.User)
                .Include(e => e.EventInterests)
                .ThenInclude(ei => ei.Interest)
                .Where(e => e.IsPublic);

            var interestIds = new List<int>();
            if (interests != null)
            {
                interestIds = await _context.Interests
                .Where(i => interests.Contains(i.Name))
                .Select(i => i.Id)
                .ToListAsync();
            }
            
            if (ageMin != null && ageMax != null)
            {
                if (ageMin > ageMax) return new List<ReadEventDto>();

                query = query.Where(e => e.AgeRangeMax >= ageMin && e.AgeRangeMin <= ageMax);
            }
            else if (ageMin != null)
            {
                query = query.Where(e => e.AgeRangeMax >= ageMin);
            }
            else if (ageMax != null)
            {
                query = query.Where(e => e.AgeRangeMin >= ageMax);
            }

            if (interests != null && interests.Any(i => !string.IsNullOrEmpty(i)))
            {
                query = query.Where(e => e.EventInterests!.Any(i => interestIds.Contains(i.EventId)));
            }

            var result = await query.ToListAsync();

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
                Img = r.Img,
                Interests = r.EventInterests?.Select(i => i.Interest != null ? i.Interest.Name : "").ToList(),
                EventParticipants = r.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = p.User?.UserName,
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
                .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.User)
                .Include(e => e.EventInterests)
                .ThenInclude(ei => ei.Interest)
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
                Img = e.Img,
                Interests = e.EventInterests?.Select(i => i.Interest != null ? i.Interest.Name : "").ToList(),
                EventParticipants = e.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = p.User?.UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = e.IsPublic
            }).ToList();

            return eventList;
        }

        public async Task<IEnumerable<ReadEventDto>> GetUnjoinedEvents(List<string?> interests, int? ageMin, int? ageMax)
        {
            var query = _context.Events
                .Include(e => e.EventParticipants)
                    .ThenInclude(ep => ep.User)
                .Include(e => e.EventInterests)
                    .ThenInclude(ei => ei.Interest)
                .Where(e => e.IsPublic &&
                    !e.EventParticipants.Any(ep => ep.UserId == UserId));


            var interestIds = new List<int>();
            if (interests != null)
            {
                interestIds = await _context.Interests
                .Where(i => interests.Contains(i.Name))
                .Select(i => i.Id)
                .ToListAsync();
            }

            if (ageMin != null && ageMax != null)
            {
                if (ageMin > ageMax) return new List<ReadEventDto>();

                query = query.Where(e => e.AgeRangeMax >= ageMin && e.AgeRangeMin <= ageMax);
            }
            else if (ageMin != null)
            {
                query = query.Where(e => e.AgeRangeMax >= ageMin);
            }
            else if (ageMax != null)
            {
                query = query.Where(e => e.AgeRangeMin >= ageMax);
            }

            if (interests != null && interests.Any(i => !string.IsNullOrEmpty(i)))
            {
                query = query.Where(e => e.EventInterests!.Any(i => interestIds.Contains(i.EventId)));
            }

            var result = await query.ToListAsync();

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
                Img = r.Img,
                Interests = r.EventInterests?.Select(i => i.Interest != null ? i.Interest.Name : "").ToList(),
                EventParticipants = r.EventParticipants.Select(p => new EventParticipantDto
                {
                    UserName = p.User?.UserName,
                    Role = p.Role,
                }).ToList(),
                IsPublic = r.IsPublic
            }).ToList();

            return eventList;
        }

        public async Task<ReadEventDto?> ReadEvent(int id)
        {
            var result = await _context.Events
                .Include(e => e.EventInterests)
                .ThenInclude(ei => ei.Interest)
                .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.User)
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
                    Img = result.Img,
                    Interests = result.EventInterests?.Select(i => i.Interest != null ? i.Interest.Name : "").ToList(),
                    EventParticipants = result.EventParticipants.Select(p => new EventParticipantDto
                    {
                        UserName = p.User?.UserName,
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
                    Img = result.Img,
                    Interests = result.EventInterests?.Select(i => i.Interest != null ? i.Interest.Name : "").ToList(),
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
                .Include(e => e.EventInterests)
                .FirstOrDefaultAsync(e => e.Id == id);

            var interests = new List<Interest>();

            if (updateEvent.Interests != null)
            {
                interests = await _context.Interests
                    .Where(i => updateEvent.Interests.Contains(i.Name))
                    .ToListAsync();
            }

            if (evnt != null)
            {
                foreach (var interest in interests)
                {
                    var eventInterest = new EventInterest
                    {
                        EventId = id,
                        InterestId = interest.Id,
                    };

                    evnt.EventInterests?.Add(eventInterest);
                }

                evnt.CreatedByUserId = UserId;
                evnt.Title = updateEvent.Title;
                evnt.Description = updateEvent.Description;
                evnt.Location = updateEvent.Location;
                evnt.AgeRangeMax = updateEvent.AgeRangeMax;
                evnt.AgeRangeMin = updateEvent.AgeRangeMin;
                evnt.IsPublic = updateEvent.IsPublic;
                evnt.StartTime = updateEvent.StartTime;
                evnt.EndTime = updateEvent.EndTime;
                evnt.Img = updateEvent.Img;

                _context.Update(evnt);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> JoinEvent(int id)
        {
            var result = await _context.Events.Include(e => e.EventParticipants).SingleOrDefaultAsync(e => e.Id == id);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == UserId);
            var canJoin = false;

            if (result == null || user == null || result.EventParticipants.Any(p => p.UserId == UserId)) return false;

            if (result.IsPublic)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - user.DateOfBirth.Year;
                if (user.DateOfBirth > today.AddYears(-age)) age--;

                if (result.AgeRangeMax == null && result.AgeRangeMin == null) canJoin = true;
                else if (age >= result.AgeRangeMin && age <= result.AgeRangeMax) canJoin = true;
                else if (result.AgeRangeMin == null && age <= result.AgeRangeMax) canJoin = true;
                else if (result.AgeRangeMax == null && age >= result.AgeRangeMin) canJoin = true;
                else canJoin = false;
                
                if (canJoin)
                {
                    var participant = new EventParticipant()
                    {
                        UserId = UserId,
                        EventId = result.Id
                    };

                    result.EventParticipants.Add(participant);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            else
            {
                var host = result.EventParticipants.SingleOrDefault(e => e.Role == "Host");
                if (host == null) return false;

                var friendship = await _context.Friendships.SingleOrDefaultAsync(f => (f.UserId == UserId && f.FriendId == host.UserId) || (f.UserId == host.UserId && f.FriendId == UserId));
                if (friendship == null) return false;

                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - user.DateOfBirth.Year;
                if (user.DateOfBirth > today.AddYears(-age)) age--;

                if (result.AgeRangeMax == null && result.AgeRangeMin == null) canJoin = true;
                else if (age >= result.AgeRangeMin && age <= result.AgeRangeMax) canJoin = true;
                else if (result.AgeRangeMin == null && age <= result.AgeRangeMax) canJoin = true;
                else if (result.AgeRangeMax == null && age >= result.AgeRangeMin) canJoin = true;
                else canJoin = false;

                if (canJoin)
                {
                    var participant = new EventParticipant()
                    {
                        UserId = UserId,
                        EventId = result.Id
                    };

                    result.EventParticipants.Add(participant);
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        public async Task<bool> LeaveEvent(int id)
        {
            var result = await _context.Events.Include(e => e.EventParticipants).SingleOrDefaultAsync(e => e.Id == id);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == UserId);
            if (result == null || user == null) return false;

            var participant = result.EventParticipants.SingleOrDefault(ep => ep.UserId == UserId);

            if (participant != null)
            {
                result.EventParticipants.Remove(participant);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
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

        public async Task<bool> HostDeleteEvent(int id)
        {
            var deleteEvent = await _context.Events
                .Include(e => e.EventParticipants)
                .SingleOrDefaultAsync(e => e.Id == id && e.CreatedByUserId == UserId);

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

        public async Task<List<string>> GetInterests()
        {
            return await _context.Interests.Select(i => i.Name).ToListAsync();
        }

        public async Task<List<int>> EventPartcipantStatus()
        {
            var events = await _context.Events
                .Include(e => e.EventParticipants)
                .Where(e => e.EventParticipants.Any(ep => ep.UserId == UserId))
                .Select(e => e.Id)
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<ReadEventDto>> GetMyCreatedEvents()
        {
            var events = await _context.Events
                .Where(e => e.CreatedByUserId == UserId)
                .Select(e => new ReadEventDto
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    UserId = e.CreatedByUserId,
                    Img = e.Img
                })
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<ReadEventDto>> GetMyJoinedEvents()
        {
            var events = await _context.Events
                .Where(e => e.EventParticipants.Any(ep => ep.UserId == UserId) && e.CreatedByUserId != UserId)
                .Select(e => new ReadEventDto
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    UserId = e.CreatedByUserId,
                    Img = e.Img
                })
                .ToListAsync();

            return events;
        }

        public async Task<bool> SendMessage(int id, string text)
        {
            var evnt = await _context.Events
                .Include(e => e.EventParticipants)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evnt == null) return false;

            var isParticipant = evnt.EventParticipants.Any(ep => ep.UserId == UserId);
            if (!isParticipant) return false;

            var message = new Message()
            {
                Content = text,
                SenderId = UserId
            };

            if (message != null)
            {
                await _context.AddAsync(message);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
