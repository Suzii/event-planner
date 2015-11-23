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
        /// <summary>
        /// Stores passed event entity to database.
        /// Sets date created to now and organizer id to passed user id.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Event> CreateEventAsync(Event e, string userId);

        Task<Event> UpdateEventAsync(Event e);

        /// <summary>
        /// V ostatních metodách nevracet Eventy, které jsou disabled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DisableEventAsync(Guid id);

        Task<Event> GetFullEventAsync(Guid id);

        Task<Event> GetEventInfoAsync(Guid id);

        Guid GetEventId(string eventHash);
    }
}
