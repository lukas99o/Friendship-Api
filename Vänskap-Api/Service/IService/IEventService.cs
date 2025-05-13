using Vänskap_Api.Models.Dtos.Event;

namespace Vänskap_Api.Service.IService
{
    public interface IEventService
    {
        Task<ReadEventDto> CreateEvent(EventDto createEvent);
        Task<IEnumerable<ReadEventDto>> ReadAllEvents();
        Task<ReadEventDto?> ReadEvent(int id);
        Task<bool> UpdateEvent(int id, EventDto updateEvent);
        Task<bool> DeleteEvent(int id);
    }
}
