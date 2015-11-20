using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class VotingService : IVotingService
    {
        private readonly VoteForDateRepository _voteForDateRepository;
        private readonly VoteForPlaceRepository _voteForPlaceRepository;

        public VotingService()
        {
            _voteForDateRepository = new VoteForDateRepository();
            _voteForPlaceRepository = new VoteForPlaceRepository();
        }

        public Task<int> GetTotalNumberOfVotersForEvent(Guid eventId)
        {
            throw new NotImplementedException();
        }

        Task<IList<VoteForDate>> IVotingService.GetVotesForDatesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        Task<IList<VoteForPlace>> IVotingService.GetVotesForPlacesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId)
        {
            throw new NotImplementedException();
        }

        public Task SubmitDateVotesByAsync(string personId, IList<VoteForDate> voteForDates)
        {
            throw new NotImplementedException();
        }

        public Task SubmitPlaceVotesByAsync(string personId, IList<VoteForPlace> voteForPlaces)
        {
            throw new NotImplementedException();
        }

        public Task SubmitVoteForDate(VoteForDate voteForDate)
        {
            throw new NotImplementedException();
        }

        public Task SubmitVoteForPlace(VoteForPlace voteForPlace)
        {
            throw new NotImplementedException();
        }
    }
}