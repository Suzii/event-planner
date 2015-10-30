using System;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        public async Task<Event> CreateEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public async Task<Event> UpdateEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public async Task DisableEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> GetEventId(string eventHash)
        {
            throw new NotImplementedException();
        }
    }
}