using MasterPiece.Models;

namespace MasterPiece.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task<string> GetEventHighLights(int eventId);  
        Task<IEnumerable<EventSession>> GetEventSchedule(int eventId);



    }


}
