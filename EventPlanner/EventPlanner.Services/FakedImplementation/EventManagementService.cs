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
        public async Task<Event> CreateEventAsync(Event e)
        {
            e.Id = Guid.NewGuid();
            return e;
        }

        public async Task<Event> UpdateEventAsync(Event e)
        {
            return e;
        }

        public async Task DisableEventAsync(Guid id)
        {
            
        }

        public async Task<Event> GetEventAsync(Guid id)
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
                OrganizerId = Guid.NewGuid().ToString()
            };
        }

        public Guid GetEventId(string eventHash)
        {
            return Guid.NewGuid();
        }
    }
}

