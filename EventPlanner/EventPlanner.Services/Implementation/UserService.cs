using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly EventRepository _eventRepository;

        public UserService() : this(new EventRepository())
        {
        }

        public UserService(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IList<Event>> GetEventsCreatedBy(string userId)
        {
            return await _eventRepository.GetByOrganizer(userId);
        }

        public async Task<bool> IsEventEditableFor(Guid eventId, string userId)
        {
            var e = await _eventRepository.GetEventInfo(eventId);
            return e.OthersCanEdit || e.OrganizerId == userId;
        }
    }
}