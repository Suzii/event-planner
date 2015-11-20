using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.FakedImplementation
{
    public class VotingService : IVotingService
    {
        private readonly IEventManagementService _eventManagementService;

        public VotingService()
        {
            _eventManagementService = new FakedImplementation.EventManagementService();
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
            return ev.TimeSlots.First().VotesForDate;
        }

        public async Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            return ev.Places.First().VotesForPlace;
        }

        public async Task SubmitDateVotesByAsync(string personId, IList<VoteForDate> voteForDates)
        {
            
        }

        public async Task SubmitPlaceVotesByAsync(string personId, IList<VoteForPlace> voteForPlaces)
        {
        }

        public async Task SubmitVoteForDate(VoteForDate voteForDate)
        {
        }

        public async Task SubmitVoteForPlace(VoteForPlace voteForPlace)
        {
        }
    }
}
