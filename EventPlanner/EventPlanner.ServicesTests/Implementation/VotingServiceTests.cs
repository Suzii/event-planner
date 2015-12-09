using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventPlanner.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using System.Diagnostics;

namespace EventPlanner.Services.Implementation.Tests
{
    [TestClass()]
    public class VotingServiceTests
    {
        private  Mock<VoteForDateRepository> _voteForDateRepository;
        private  Mock<VoteForPlaceRepository> _voteForPlaceRepository;
        private  Mock<IEventDetailsService> _eventDetailsService;
        private  VotingService _votingService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _voteForDateRepository = new Mock<VoteForDateRepository>();
            _voteForPlaceRepository = new Mock<VoteForPlaceRepository>();
            _eventDetailsService = new Mock<IEventDetailsService>();
            _votingService = new VotingService(_eventDetailsService.Object, _voteForDateRepository.Object, _voteForPlaceRepository.Object);
        }
        
        [TestMethod()]
        public async Task GetTotalNumberOfVotersForEventTest()
        {
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            VoteForPlace vp1 = new VoteForPlace { Id = new Guid("00000000-0000-0000-0000-000000000000"), UserId = "0" };
            VoteForPlace vp2 = new VoteForPlace { Id = new Guid("00000000-0000-0000-0000-000000000001"), UserId = "1" };
            Place p1 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000000"), Event = e, EventId = eId, VotesForPlace = new List<VoteForPlace> { vp1, vp2 } };
            Place p2 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000001"), Event = e, EventId = eId };
            List<Place> places = new List<Place> { p1, p2 };
            VoteForDate vd1 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000000"), UserId = "1" };
            VoteForDate vd2 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000001"), UserId = "2" };
            TimeSlot t1 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000000"), Event = e, EventId = eId, VotesForDate = new List<VoteForDate> { vd1, vd2 } };
            TimeSlot t2 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000001"), Event = e, EventId = eId };
            List<TimeSlot> timeslots = new List<TimeSlot> { t1, t2 };
            var tcs1 = new TaskCompletionSource<IList<Place>>();
            tcs1.SetResult(places);
            var tcs2 = new TaskCompletionSource<IList<TimeSlot>>();
            tcs2.SetResult(timeslots);

            _eventDetailsService.Setup(mock => mock.GetPlacesWithVotes(eId)).Returns(tcs1.Task);
            _eventDetailsService.Setup(mock => mock.GetDatesWithVotes(eId)).Returns(tcs2.Task);
            var task = await _votingService.GetTotalNumberOfVotersForEvent(eId);
            _eventDetailsService.Verify(mock => mock.GetPlacesWithVotes(eId));
            _eventDetailsService.Verify(mock => mock.GetDatesWithVotes(eId));

            Assert.AreEqual(3, task);

        }

        [TestMethod()]
        public async Task GetVotesForDateAsyncTest()
        {
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            TimeSlot t1 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000001"), EventId = eId, Event = e };
            TimeSlot t2 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000002"), EventId = eId, Event = e };
            VoteForDate vd1 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000000"), TimeSlotId = t1.Id, TimeSlot = t1};
            VoteForDate vd2 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000002"), TimeSlotId = t1.Id, TimeSlot = t1 };
            VoteForDate vd3 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000003"), TimeSlotId = t2.Id, TimeSlot = t2 };
            var tcs = new TaskCompletionSource<List<VoteForDate>>();
            tcs.SetResult(new List<VoteForDate> { vd1, vd2, vd3 });
            _voteForDateRepository.Setup(mock => mock.GetAll()).Returns(tcs.Task);
            var task = await _votingService.GetVotesForDateAsync(eId, t1.Id);
            _voteForDateRepository.Verify(mock => mock.GetAll());

            CollectionAssert.AreEqual(new List<VoteForDate> { vd1, vd2 }, task.ToList());
        }

        [TestMethod()]
        public async Task GetVotesForPlaceAsyncTest()
        {
            Guid eId = new Guid();
            Event e = new Event { Id = eId };
            Place p1 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000001"), EventId = eId, Event = e };
            Place p2 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000002"), EventId = eId, Event = e };
            VoteForPlace vp1 = new VoteForPlace { Id = new Guid("00000000-0000-0000-0000-000000000000"),  PlaceId = p1.Id, Place = p1 };
            VoteForPlace vp2 = new VoteForPlace { Id = new Guid("00000000-0000-0000-0000-000000000002"), PlaceId = p1.Id, Place = p1 };
            VoteForPlace vp3 = new VoteForPlace{ Id = new Guid("00000000-0000-0000-0000-000000000003"), PlaceId = p2.Id, Place = p2 };
            var tcs = new TaskCompletionSource<List<VoteForPlace>>();
            tcs.SetResult(new List<VoteForPlace> { vp1, vp2, vp3 });
            _voteForPlaceRepository.Setup(mock => mock.GetAll()).Returns(tcs.Task);
            var task = await _votingService.GetVotesForPlaceAsync(eId, p1.Id);
            _voteForPlaceRepository.Verify(mock => mock.GetAll());

            CollectionAssert.AreEqual(new List<VoteForPlace> { vp1, vp2 }, task.ToList());
        }

        [TestMethod()]
        public async Task SubmitVoteForDateTest()
        {
            TimeSlot t1 = new TimeSlot { Id = new Guid("00000000-0000-0000-0000-000000000001") };
            VoteForDate vd1 = new VoteForDate { Id = new Guid("00000000-0000-0000-0000-000000000000"), TimeSlotId = t1.Id, TimeSlot = t1 };
            var tcs = new TaskCompletionSource<VoteForDate>();
            tcs.SetResult(vd1);
            _voteForDateRepository.Setup(mock => mock.AddOrUpdate(vd1)).Returns(tcs.Task);
            var task = await _votingService.SubmitVoteForDate(vd1);
            _voteForDateRepository.Verify(mock => mock.AddOrUpdate(vd1));

            Assert.AreEqual(task, vd1);
        }

        [TestMethod()]
        public async Task SubmitVoteForPlaceTest()
        {
            Place p1 = new Place { Id = new Guid("00000000-0000-0000-0000-000000000001") };
            VoteForPlace vp1 = new VoteForPlace { Id = new Guid("00000000-0000-0000-0000-000000000000"), PlaceId = p1.Id, Place = p1 };
            var tcs = new TaskCompletionSource<VoteForPlace>();
            tcs.SetResult(vp1);
            _voteForPlaceRepository.Setup(mock => mock.AddOrUpdate(vp1)).Returns(tcs.Task);
            var task = await _votingService.SubmitVoteForPlace(vp1);
            _voteForPlaceRepository.Setup(mock => mock.AddOrUpdate(vp1));

            Assert.AreEqual(task, vp1);
        }
    }
}