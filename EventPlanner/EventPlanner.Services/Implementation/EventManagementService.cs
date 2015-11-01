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

        public async Task<Event> CreateEventAsync(Event e)
        {
            return await _eventRepository.AddOrUpdate(e);
        }

        public async Task<Event> UpdateEventAsync(Event e)
        {
            return await _eventRepository.AddOrUpdate(e);
        }

        public async Task DisableEventAsync(Guid id)
        {
            var e = await _eventRepository.GetEvent(id);
            e.Disabled = true;
            await _eventRepository.AddOrUpdate(e);
        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            return await _eventRepository.GetEvent(id);
        }

        public Guid GetEventId(string eventHash)
        {
            return new Guid(Convert.FromBase64String(eventHash));
        }
    }
}