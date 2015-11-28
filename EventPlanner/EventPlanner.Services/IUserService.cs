using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// Interface containing methods handling database operations for specific user
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get event created by user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>List of Events created by user</returns>
        Task<IList<Event>> GetEventsCreatedBy(string userId);

        /// <summary>
        /// Get bool value indicating event is editable for given user
        /// </summary>
        /// <param name="eventId">Id of event</param>
        /// <param name="userId">Id of user</param>
        /// <returns>True, if event is editable for specific user, otherwise false</returns>
        Task<bool> IsEventEditableFor(Guid eventId, string userId);
    }
}
