using System;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        private readonly EventRepository _eventRepository;

        public EventManagementService()
        {
            _eventRepository = new EventRepository();
        }

        public async Task<Event> CreateEventAsync(Event e, string userId)
        {
            e.OrganizerId = userId;
            e.Created = DateTime.Now;
            return await _eventRepository.AddOrUpdate(e);
        }

        public async Task<Event> UpdateEventAsync(Event e)
        {
            //TODO: problem with created date == null.. leads to failed update
            //HOTFIX just to be able to test update functionality
            var oldEvent = await _eventRepository.GetEventInfo(e.Id);
            e.Created = oldEvent.Created;
            e.OrganizerId = oldEvent.OrganizerId;
            return await _eventRepository.AddOrUpdate(e);
        }

        public async Task DisableEventAsync(Guid id)
        {
            var e = await _eventRepository.GetEventInfo(id);
            e.Disabled = true;
            await _eventRepository.AddOrUpdate(e);
        }

        public async Task<Event> GetFullEventAsync(Guid id)
        {
            return await _eventRepository.GetFullEvent(id);
        }

        public Guid GetEventId(string eventHash)
        {
            eventHash = eventHash.Replace("_", "/");
            eventHash = eventHash.Replace("-", "+");
            var buffer = Convert.FromBase64String(eventHash + "==");
            return new Guid(buffer);
        }
    }
}