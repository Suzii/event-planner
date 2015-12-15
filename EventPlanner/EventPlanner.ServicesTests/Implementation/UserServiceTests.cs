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
    public class UserServiceTests
    {
        private Mock<EventRepository> _eventRepository;
        private UserService _userService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _eventRepository = new Mock<EventRepository>();
            _userService = new UserService(_eventRepository.Object);
        }

        [TestMethod()]
        public async Task GetEventsCreatedByTest()
        {
            var tcs = new TaskCompletionSource<List<Event>>();
            string userId = "1";
            Event e1 = new Event { Title = "New Event", OrganizerId = userId };
            Event e2 = new Event { Title = "New Event2", OrganizerId = userId };
            List<Event> events = new List<Event> { e1, e2 };
            tcs.SetResult(events);

            _eventRepository.Setup(mock => mock.GetByOrganizer(userId)).Returns(tcs.Task);
            var task = await _userService.GetEventsCreatedBy(userId);
            _eventRepository.Verify(mock => mock.GetByOrganizer(userId), Times.Once(), "Method GetByOrganizer was not called or was called more than once (or its parameters were wrong).");
            CollectionAssert.AreEqual(events, task.ToList(), "The list of events returned by GetEventsCreatedBy was different than expected");
        }

        [TestMethod()]
        public async Task IsEventEditableForTest()
        {
            var tcs = new TaskCompletionSource<Event>();
            string userId = "u1";
            string userId2 = "u2";
            Guid e1Id = new Guid("00000000-0000-0000-0000-000000000000");
            Guid e2Id = new Guid("00000000-0000-0000-0000-000000000001");
            Event e1 = new Event { Id = e1Id, Title = "New Event", OthersCanEdit = true };
            Event e2 = new Event { Id = e2Id, Title = "New Event2", OthersCanEdit = false, OrganizerId = userId };
            tcs.SetResult(e1);

            _eventRepository.Setup(mock => mock.GetEventInfo(e1Id)).Returns(tcs.Task);
            var task = await _userService.IsEventEditableFor(e1Id, userId);
            _eventRepository.Verify(mock => mock.GetEventInfo(e1Id), Times.Once());
            Assert.AreEqual(true, task, "This event should be editable, but the method IsEventEditable returns false.");

            var tcs2 = new TaskCompletionSource<Event>();
            tcs2.SetResult(e2);
            _eventRepository.Setup(mock => mock.GetEventInfo(e2Id)).Returns(tcs2.Task);
            var task2 = await _userService.IsEventEditableFor(e2Id, userId);
            _eventRepository.Verify(mock => mock.GetEventInfo(e2Id), Times.Once(), "Method GetEventInfo was not called or was called more than once (or its parameters were wrong).");
            Assert.AreEqual(true, task2, "This event should be editable, but the method IsEventEditable returns false.");

            _eventRepository.Setup(mock => mock.GetEventInfo(e2Id)).Returns(tcs2.Task);
            var task3 = await _userService.IsEventEditableFor(e2Id, userId2);
            _eventRepository.Verify(mock => mock.GetEventInfo(e2Id), Times.Exactly(2), "Method GetEventInfo was not called or was called more than once (or its parameters were wrong).");
            Assert.AreEqual(false, task3, "This event should not be editable, but the method IsEventEditable returns true.");
        }
    }
}