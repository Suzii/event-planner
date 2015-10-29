using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.FakedImplementation
{
    public class EventManagementService : IEventManagementService
    {
        public Event CreateEvent(Event e)
        {
            e.Id = Guid.NewGuid();
            return e;
        }

        public Event UpdateEvent(Event e)
        {
            return e;
        }

        public void DisableEvent(Guid id)
        {
            
        }

        public Event GetEvent(Guid id)
        {
            return new Event()
            {
                Id = id,
                Title = "Faked event",
                Desc = "This event has no purpose at all and only serves for faked implementation of our cute EventManagementService",
                Created = DateTime.Today.AddDays(-2),
                OthersCanEdit = true,
                ExpectedLength = 2,
                TimeSlots = null,
                Places = new List<Place>()
                {
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "something", VotesForPlace = null},
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "something else", VotesForPlace = null}
                },
                OrganizerId = Guid.NewGuid()
            };
        }

        public Guid GetEventId(string eventHash)
        {
            return Guid.NewGuid();
        }
    }
}

