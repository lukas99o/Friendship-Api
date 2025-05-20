using Vänskap_Api.Models.Dtos.Event;

namespace Vänskap_Api.Service.IService
{
    public interface IEventService
    {
        Task<ReadEventDto> CreateEvent(EventDto createEvent);
        Task<IEnumerable<ReadEventDto>> ReadAllPublicEvents(List<string?> interests, int? ageMin, int? ageMax);
        Task<IEnumerable<ReadEventDto>> GetAllFriendEvents();
        Task<ReadEventDto?> ReadEvent(int id);
        Task<bool> UpdateEvent(int id, EventDto updateEvent);
        Task<bool> JoinEvent(int id);
        Task<bool> LeaveEvent(int id);
        Task<bool> DeleteEvent(int id);
        Task<List<string>> GetInterests();
    }
}
