using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.FakedImplementation
{
    public class VotingService : IVotingService
    {
        private readonly IEventManagementService _eventManagementService;
        private readonly VoteForDateRepository _voteForDateRepository;
        private readonly VoteForPlaceRepository _voteForPlaceRepository;

        public VotingService()
        {
            _voteForDateRepository = new VoteForDateRepository();
            _voteForPlaceRepository = new VoteForPlaceRepository();
            _eventManagementService = new Services.Implementation.EventManagementService();
        }

        public async Task<int> GetTotalNumberOfVotersForEvent(Guid eventId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            var placesTotalVoters = ev.Places.Where(p => p.VotesForPlace != null).SelectMany(p => p.VotesForPlace?.Select(v => v.UserId).ToList()).Distinct();
            var datesTotalVoters = ev.TimeSlots.Where(p => p.VotesForDate!= null).SelectMany(p => p.VotesForDate?.Select(v => v.UserId).ToList()).Distinct();

            return placesTotalVoters.Union(datesTotalVoters).Distinct().Count();
        }

        public async Task<IList<VoteForDate>> GetVotesForDatesAsync(Guid eventId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            return ev.TimeSlots.SelectMany(ts => ts.VotesForDate).ToList();
        }

        public async Task<IList<VoteForPlace>> GetVotesForPlacesAsync(Guid eventId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            return ev.Places.SelectMany(ts => ts.VotesForPlace).ToList();
        }

        public async Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            return ev.TimeSlots.Where(ts => ts.Id == dateId).SelectMany(ts => ts.VotesForDate).ToList();
        }

        public async Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            return ev.Places.Where(p => p.Id == placeId).SelectMany(ts => ts.VotesForPlace).ToList();

        }

        public async Task SubmitDateVotesByAsync(IList<VoteForDate> voteForDates)
        {
            foreach(var vote in voteForDates)
            {
                await SubmitVoteForDate(vote);
            }
        }

        public async Task SubmitPlaceVotesByAsync(IList<VoteForPlace> voteForPlaces)
        {
            foreach (var vote in voteForPlaces)
            {
                await SubmitVoteForPlace(vote);
            }
        }

        public async Task<VoteForDate> SubmitVoteForDate(VoteForDate voteForDate)
        {
            return await _voteForDateRepository.AddOrUpdate(voteForDate);
        }

        public async Task<VoteForPlace> SubmitVoteForPlace(VoteForPlace voteForPlace)
        {
            return await _voteForPlaceRepository.AddOrUpdate(voteForPlace);
        }
    }
}
