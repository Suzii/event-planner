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
        public EventEntity CreateEvent(EventEntity e)
        {
            e.Id = Guid.NewGuid();
            return e;
        }

        public EventEntity UpdateEvent(EventEntity e)
        {
            return e;
        }

        public void DisableEvent(Guid id)
        {
            
        }

        public EventEntity GetEntity(Guid id)
        {
            return new EventEntity()
            {
                Id = id,
                Title = "Faked event",
                Desc = "This event has no purpose at all and only serves for faked implementation of our cute EventManagementService",
                Created = DateTime.Today.AddDays(-2),
                OthersCanEdit = true,
                ExpectedLength = 2,
                TimeSlots = null,
                Places = new List<PlaceEntity>()
                {
                    new PlaceEntity() { Id = Guid.NewGuid(), EventId = id, VenueId = "something", VotesForPlace = null},
                    new PlaceEntity() { Id = Guid.NewGuid(), EventId = id, VenueId = "something else", VotesForPlace = null}
                },
                OrganizerId = Guid.NewGuid()
            };
        }
    }
}

