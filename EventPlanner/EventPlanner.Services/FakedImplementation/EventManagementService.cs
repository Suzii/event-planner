using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Enums;

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
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Now.AddDays(-1), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Now, VotesForDate = null }
                },
                Places = new List<Place>()
                {
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "529ebe0f498eee32aa9dee7e", VotesForPlace = null},
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "51470131e4b0ff6e39c2fb73", VotesForPlace = null},
                    new Place() { Id = Guid.NewGuid(), EventId = id, VenueId = "4b315c46f964a520700525e3", VotesForPlace = null}
                },
                OrganizerId = Guid.NewGuid().ToString()
            };



            ev.Places[0].VotesForPlace = new List<VoteForPlace>()
                {
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Tomas", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Janka", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Jirka", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Martin", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Suzi", WillAttend = false},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Parmezan", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Matho", WillAttend = false},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "JayDee", WillAttend = true}
                };

            ev.Places[1].VotesForPlace = new List<VoteForPlace>()
                {
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Tomas", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Janka", WillAttend = false},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Jirka", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Martin", WillAttend = false},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Suzi", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Parmezan", WillAttend = false},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Matho", WillAttend = true},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "JayDee", WillAttend = false}
                };

            ev.TimeSlots[0].VotesForDate = new List<VoteForDate>()
            {
                new VoteForDate() {TimeSlotId = ev.TimeSlots[0].Id, UserId = "Tomas", WillAttend = WillAttend.Yes},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[0].Id, UserId = "Jirka", WillAttend = WillAttend.Maybe},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[0].Id, UserId = "Jana", WillAttend = WillAttend.Yes},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[0].Id, UserId = "Suzi", WillAttend = WillAttend.No},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[0].Id, UserId = "Martin", WillAttend = WillAttend.No},
            };

            ev.TimeSlots[1].VotesForDate = new List<VoteForDate>()
            {
                new VoteForDate() {TimeSlotId = ev.TimeSlots[1].Id, UserId = "Tomas", WillAttend = WillAttend.Maybe},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[1].Id, UserId = "Jirka", WillAttend = WillAttend.Maybe},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[1].Id, UserId = "Jana", WillAttend = WillAttend.Yes},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[1].Id, UserId = "Suzi", WillAttend = WillAttend.Yes},
                new VoteForDate() {TimeSlotId = ev.TimeSlots[1].Id, UserId = "Martin", WillAttend = WillAttend.No},
            };

            return ev;
        }

        public Guid GetEventId(string eventHash)
        {
            return Guid.NewGuid();
        }
    }
}

