using System;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        public Event CreateEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public Event UpdateEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public void DisableEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Event GetEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid GetEventId(string eventHash)
        {
            throw new NotImplementedException();
        }
    }
}