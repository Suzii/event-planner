using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Enums;

namespace EventPlanner.Services.FakedImplementation
{
    public class EventManagementService : IEventManagementService
    {
        private readonly EventRepository _eventRepository;

        public EventManagementService()
        {
            _eventRepository = new EventRepository();
        }
        public async Task<Event> CreateEventAsync(Event e, string userId)
        {
            var ev = await GetFullEventAsync(Guid.Empty);
            ev.OrganizerId = userId;
            ev.Id = Guid.Empty;
            return await _eventRepository.AddOrUpdate(ev);
        }

        public async Task<Event> UpdateEventAsync(Event e)
        {
            return e;
        }

        public async Task DisableEventAsync(Guid id)
        {
            
        }

        public async Task<Event> GetFullEventAsync(Guid id)
        {
            var ev = new Event()
            {
                Id = id,
                Title = "Faked event",
                Desc = "This event has no purpose at all and only serves for faked implementation of our cute EventManagementService",
                CreatedOn = DateTime.Today.AddDays(-2),
                OthersCanEdit = true,
                ExpectedLength = 2,
                TimeSlots = new List<TimeSlot>()
                {
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-1).AddHours(1), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-1).AddHours(2), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-1).AddHours(3), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-2).AddHours(10), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-2).AddHours(11), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-3).AddHours(20), VotesForDate = null },
                    new TimeSlot() {Id = Guid.NewGuid(), EventId = id, DateTime = DateTime.Today.AddDays(-3).AddHours(21), VotesForDate = null }
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
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Tomas", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Janka", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Jirka", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Martin", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Suzi", WillAttend = WillAttend.No},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Parmezan", WillAttend = WillAttend.Maybe},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "Matho", WillAttend = WillAttend.No},
                    new VoteForPlace() {PlaceId = ev.Places[0].Id, UserId = "JayDee", WillAttend = WillAttend.Maybe}
                };

            ev.Places[1].VotesForPlace = new List<VoteForPlace>()
                {
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Tomas", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Janka", WillAttend = WillAttend.No},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Jirka", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Martin", WillAttend = WillAttend.Maybe},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Suzi", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Parmezan", WillAttend = WillAttend.No},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "Matho", WillAttend = WillAttend.Yes},
                    new VoteForPlace() {PlaceId = ev.Places[1].Id, UserId = "JayDee", WillAttend = WillAttend.No}
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

        public async Task<Event> GetEventInfoAsync(Guid id)
        {
            var ev = await GetFullEventAsync(Guid.Empty);
            ev.TimeSlots = null;
            ev.Places = null;
            return ev;
        }

        public Guid GetEventId(string eventHash)
        {
            return Guid.NewGuid();
        }
    }
}

