using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public EventEntity GetEntity(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}