using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventPlanner.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using Moq;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation.Tests
{
    [TestClass()]
    public class EventDetailsServiceTests
    {
        private Mock<PlaceRepository> _placeRepository;
        private Mock<TimeSlotRepository> _timeSlotRepository;
        private EventDetailsService _eventDetailsService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _placeRepository = new Mock<PlaceRepository>();
            _timeSlotRepository = new Mock<TimeSlotRepository>();
            _eventDetailsService = new EventDetailsService(_placeRepository.Object, _timeSlotRepository.Object);
        }

        [TestMethod()]
        public async Task GetPlacesTest()
        {
            var tcs = new TaskCompletionSource<List<Place>>();
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            Place p1 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000000"), EventId = eId, Event = e };
            Place p2 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000001"), EventId = eId, Event = e };
            List<Place> places = new List<Place> { p1, p2 };
            tcs.SetResult(places);

            _placeRepository.Setup(mock => mock.GetPlaceInfoByEvent(eId)).Returns(tcs.Task);
            var task = await _eventDetailsService.GetPlaces(eId);
            _placeRepository.Verify((mock => mock.GetPlaceInfoByEvent(eId)), Times.Once(), "Method GetPlaceInfoEvent was not called or was called more than once (or its parameters were wrong).");
            CollectionAssert.AreEqual(task.ToList(), places, "Method GetPlaces returned different places than expected.");
        }

        [TestMethod()]
        public async Task GetPlacesWithVotesTest()
        {
            var tcs = new TaskCompletionSource<List<Place>>();
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            Place p1 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000000"), EventId = eId, Event = e };
            Place p2 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000001"), EventId = eId, Event = e };
            List<Place> places = new List<Place> { p1, p2 };
            tcs.SetResult(places);

            _placeRepository.Setup(mock => mock.GetPlaceWithVotesByEvent(eId)).Returns(tcs.Task);
            var task = await _eventDetailsService.GetPlacesWithVotes(eId);
            _placeRepository.Verify((mock => mock.GetPlaceWithVotesByEvent(eId)), Times.Once(), "Method GetPlaceWithVotesByEvent was not called or was called more than once (or its parameters were wrong).");
            CollectionAssert.AreEqual(task.ToList(), places, "Method GetPlacesWithVotes returned different Places than expected");
        }

        [TestMethod()]
        public async Task GetDatesTest()
        {
            var tcs = new TaskCompletionSource<List<TimeSlot>>();
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            TimeSlot t1 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000000"), DateTime = DateTime.Now, Event = e, EventId = eId };
            TimeSlot t2 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000001"), DateTime = DateTime.Now, Event = e, EventId = eId };
            List<TimeSlot> timeslots = new List<TimeSlot> { t1, t2 };
            tcs.SetResult(timeslots);

            _timeSlotRepository.Setup(mock => mock.GetTimeSlotInfoByEvent(eId)).Returns(tcs.Task);
            var task = await _eventDetailsService.GetDates(eId);
            _timeSlotRepository.Verify(mock => mock.GetTimeSlotInfoByEvent(eId), Times.Once(), "Method GetTimeSlotInfoByEvent was not called or was called more than once (or its parameters were wrong).");
            CollectionAssert.AreEqual(task.ToList(), timeslots, "Method GetDates returned different timeslots than expected");
        }

        [TestMethod()]
        public async Task GetDatesWithVotesTest()
        {
            var tcs = new TaskCompletionSource<List<TimeSlot>>();
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            TimeSlot t1 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000000"), DateTime = DateTime.Now, Event = e, EventId = eId };
            TimeSlot t2 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000001"), DateTime = DateTime.Now, Event = e, EventId = eId };
            List<TimeSlot> timeslots = new List<TimeSlot> { t1, t2 };
            tcs.SetResult(timeslots);

            _timeSlotRepository.Setup(mock => mock.GetTimeSlotWithVotesByEvent(eId)).Returns(tcs.Task);
            var task = await _eventDetailsService.GetDatesWithVotes(eId);
            _timeSlotRepository.Verify(mock => mock.GetTimeSlotWithVotesByEvent(eId), Times.Once(), "Method GetTimeSlotWithVotesByEvent was not called or was called more than once (or its parameters were wrong).");
            CollectionAssert.AreEqual(task.ToList(), timeslots, "Method GetDatesWithVotes returned different timeslots than expected");
        }
    }
}