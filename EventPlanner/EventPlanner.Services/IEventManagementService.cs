using System;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// Interface containing methods handling EventRepository
    /// </summary>
    public interface IEventManagementService
    {
        /// <summary>
        /// Stores passed event entity to database.
        /// Sets date created to now and organizer id to passed user id.
        /// </summary>
        /// <param name="e">Event to save</param>
        /// <param name="userId">Current user id</param>
        /// <returns>Created Event</returns>
        Task<Event> CreateEventAsync(Event e, string userId);

        /// <summary>
        /// Update passed entity to database
        /// </summary>
        /// <param name="e">Event to update</param>
        /// <returns>Updated Event</returns>
        Task<Event> UpdateEventAsync(Event e);

        /// <summary>
        /// Disable Event by given ID
        /// </summary>
        /// <param name="id">Id of event to disable</param>
        Task DisableEventAsync(Guid id);

        /// <summary>
        /// Get full event with related entities by given ID
        /// </summary>
        /// <param name="id">ID of event</param>
        /// <returns>Event with related entities</returns>
        Task<Event> GetFullEventAsync(Guid id);

        /// <summary>
        /// Get basic event without related entities
        /// </summary>
        /// <param name="id">ID of event</param>
        /// <returns>Event without related entities</returns>
        Task<Event> GetEventInfoAsync(Guid id);

        /// <summary>
        /// Get event id from passed hash
        /// </summary>
        /// <param name="eventHash">Event hash</param>
        /// <returns>Event Id</returns>
        Guid GetEventId(string eventHash);
    }
}
