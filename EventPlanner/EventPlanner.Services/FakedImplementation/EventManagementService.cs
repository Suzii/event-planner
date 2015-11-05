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
        public async Task<Event> CreateEventAsync(Event e, string userID)
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
            var ev = new Event()
            {
                Id = id,
                Title = "Faked event",
                Desc = "This event has no purpose at all and only serves for faked implementation of our cute EventManagementService",
                Created = DateTime.Today.AddDays(-2),
                OthersCanEdit = true,
                ExpectedLength = 2,
                TimeSlots = new List<TimeSlot>()
                {
                    new TimeSlot()
                    {
                        Id = Guid.NewGuid(),
                        EventId = id,
                        DateTime = DateTime.Today,
                        VotesForDate = new List<VoteForDate>()
                        {
                            new VoteForDate() { Id = Guid.NewGuid(), UserId = Guid.NewGuid().ToString(), TimeSlotId = Guid.NewGuid()},
                            new VoteForDate() { Id = Guid.NewGuid(), UserId = Guid.NewGuid().ToString(), TimeSlotId = Guid.NewGuid()}
                        }
                    }
                },
                Places = new List<Place>()
                {
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "529ebe0f498eee32aa9dee7e", VotesForPlace = null},
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "51470131e4b0ff6e39c2fb73", VotesForPlace = null}
                },
                OrganizerId = Guid.NewGuid().ToString()
            };



            ev.Places[0].VotesForPlace = new List<VoteForPlace>()
                {
                    new VoteForPlace() {UserId = "Tomas", WillAttend = true},
                    new VoteForPlace() {UserId = "Janka", WillAttend = true},
                    new VoteForPlace() {UserId = "Jirka", WillAttend = true},
                    new VoteForPlace() {UserId = "Martin", WillAttend = true},
                    new VoteForPlace() {UserId = "Suzi", WillAttend = false},
                    new VoteForPlace() {UserId = "Parmezan", WillAttend = true},
                    new VoteForPlace() {UserId = "Matho", WillAttend = false},
                    new VoteForPlace() {UserId = "JayDee", WillAttend = true}
                };

            ev.Places[1].VotesForPlace = new List<VoteForPlace>()
                {
                    new VoteForPlace() {UserId = "Tomas", WillAttend = true},
                    new VoteForPlace() {UserId = "Janka", WillAttend = false},
                    new VoteForPlace() {UserId = "Jirka", WillAttend = true},
                    new VoteForPlace() {UserId = "Martin", WillAttend = false},
                    new VoteForPlace() {UserId = "Suzi", WillAttend = true},
                    new VoteForPlace() {UserId = "Parmezan", WillAttend = false},
                    new VoteForPlace() {UserId = "Matho", WillAttend = true},
                    new VoteForPlace() {UserId = "JayDee", WillAttend = false}
                };

            return ev;
        }

        public Guid GetEventId(string eventHash)
        {
            return Guid.NewGuid();
        }
    }
}

