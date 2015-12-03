using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class providing services to operate with events for specific user
    /// </summary>
    public class UserService : IUserService
    {
        private readonly EventRepository _eventRepository;

        /// <summary>
        /// Initialize new instance of UserService
        /// </summary>
        public UserService() : this(new EventRepository())
        {

        }

        /// <summary>
        /// Initialize new instance of UserService
        /// </summary>
        /// <param name="eventRepository">EventRepository to operate with</param>
        public UserService(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Get all events created by user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>List of events created by user</returns>
        public async Task<IList<Event>> GetEventsCreatedBy(string userId)
        {
            return await _eventRepository.GetByOrganizer(userId);
        }

        /// <summary>
        /// Get information that event is editable for specific user
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="userId">User Id</param>
        /// <returns>True if event is edtable for specific user, otherwise false</returns>
        public async Task<bool> IsEventEditableFor(Guid eventId, string userId)
        {
            var e = await _eventRepository.GetEventInfo(eventId);
            return e.OthersCanEdit || e.OrganizerId == userId;
        }
    }
}