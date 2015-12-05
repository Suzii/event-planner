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
    public class EventManagementServiceTests
    {
        private Mock<EventRepository> _eventRepository;
        private Mock<PlaceRepository> _placeRepository;
        private Mock<TimeSlotRepository> _timeSlotRepository;

        private EventManagementService _eventManagementService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _eventRepository = new Mock<EventRepository>();
            _placeRepository = new Mock<PlaceRepository>();
            _timeSlotRepository = new Mock<TimeSlotRepository>();

            _eventManagementService = new EventManagementService(_eventRepository.Object, _placeRepository.Object, _timeSlotRepository.Object);
        }

        [TestMethod()]
        public async Task CreateEventAsyncTest()
        {
            var tcs = new TaskCompletionSource<Event>();
            string userId = "u1";
            Event e1 = new Event { Title = "New Event" };
            tcs.SetResult(e1);

            _eventRepository.Setup(mock => mock.AddOrUpdate(e1)).Returns(tcs.Task);
            var task = await _eventManagementService.CreateEventAsync(e1, userId);
            _eventRepository.Verify(mock => mock.AddOrUpdate(e1), Times.Once());

            Assert.AreEqual(e1.Title, task.Title);
            Assert.AreEqual(userId, task.OrganizerId);
        }

        [TestMethod()]
        public void UpdateEventAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task DisableEventAsyncTest()
        {
            var tcs = new TaskCompletionSource<Event>();
            Guid eId = new Guid();
            Event e1 = new Event { Id = eId, Title = "New Event" };
            tcs.SetResult(e1);

            _eventRepository.Setup(mock => mock.GetEventInfo(eId)).Returns(tcs.Task);
            _eventRepository.Setup(mock => mock.AddOrUpdate(e1)).Returns(tcs.Task);
             await _eventManagementService.DisableEventAsync(eId);
            _eventRepository.Verify(mock => mock.GetEventInfo(eId), Times.Once);
            _eventRepository.Verify(mock => mock.AddOrUpdate(e1), Times.Once);

            Assert.AreEqual(e1.Disabled, true);
        }

        [TestMethod()]
        public void GetFullEventAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task GetEventInfoAsyncTest()
        {
            var tcs = new TaskCompletionSource<Event>();
            Guid eId = new Guid();
            Event e1 = new Event { Id = eId, Title = "New Event" };
            tcs.SetResult(e1);

            _eventRepository.Setup(mock => mock.GetEventInfo(eId)).Returns(tcs.Task);
            var task = await _eventManagementService.GetEventInfoAsync(eId);
            _eventRepository.Verify(mock => mock.GetEventInfo(eId));

            Assert.AreEqual(task, e1);
        }

        [TestMethod()]
        public void GetEventIdTest()
        {
            Assert.Fail();
        }
    }
}