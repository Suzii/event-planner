using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// Interface containing methods to operate with event detail
    /// </summary>
    public interface IEventDetailsService
    {
        /// <summary>
        /// Get places for event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of places</returns>
        Task<IList<Place>> GetPlaces(Guid eventId);

        /// <summary>
        /// Get places for event with votes for this place
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of places for event with list of associated votes populated</returns>
        Task<IList<Place>> GetPlacesWithVotes(Guid eventId);

        /// <summary>
        /// Get dates for event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of dates</returns>
        Task<IList<TimeSlot>> GetDates(Guid eventId);

        /// <summary>
        /// Get dates for event with votes for this date
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of dates with list of associated votes populated</returns>
        Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId);
    }
}
