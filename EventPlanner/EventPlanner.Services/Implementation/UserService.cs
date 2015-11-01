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

        public UserService()
        {
            _eventRepository = new EventRepository();
        }

        public async Task<IList<Event>> GetEventsCreatedBy(string userId)
        {
            return await _eventRepository.GetByOrganizer(userId.ToString());
        }

        public async Task<bool> IsEventEditableFor(Guid eventId, string userId)
        {
            var e = await _eventRepository.GetEvent(eventId);
            return (e.OthersCanEdit || e.OrganizerId == userId);
        }
    }
}