using System;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        private readonly EventRepository _repository;

        public EventManagementService()
        {
            _repository = new EventRepository();
        }

        public async Task<Event> CreateEventAsync(Event e)
        {
            return await _repository.AddOrUpdate(e);
        }

        public async Task<Event> UpdateEventAsync(Event e)
        {
            return await _repository.AddOrUpdate(e);
        }

        public async Task DisableEventAsync(Guid id)
        {
            var e = await _repository.GetEvent(id);
            e.Disabled = true;
            await _repository.AddOrUpdate(e);
        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            return await _repository.GetEvent(id);
        }

        public Guid GetEventId(string eventHash)
        {
            return new Guid(Convert.FromBase64String(eventHash));
        }
    }
}