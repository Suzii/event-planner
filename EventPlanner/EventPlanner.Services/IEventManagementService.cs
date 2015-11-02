using System;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// crud operations on events, handling logic
    /// </summary>
    public interface IEventManagementService
    {
        Task<Event> CreateEventAsync(Event e);

        Task<Event> UpdateEventAsync(Event e);

        /// <summary>
        /// V ostatních metodách nevracet Eventy, které jsou disabled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DisableEventAsync(Guid id);

        Task<Event> GetEventAsync(Guid id);

        Guid GetEventId(string eventHash);
    }
}
