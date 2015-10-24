using System;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        public EventEntity CreateEvent(EventEntity e)
        {
            throw new NotImplementedException();
        }

        public EventEntity UpdateEvent(EventEntity e)
        {
            throw new NotImplementedException();
        }

        public void DisableEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public EventEntity GetEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid GetEventId(string eventHash)
        {
            throw new NotImplementedException();
        }
    }
}