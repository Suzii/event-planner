using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Entities;
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

        public async Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId, IList<Guid> dateIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId, IList<Guid> placeIds)
        {
            throw new NotImplementedException();
        }

        public Task SubmitDateVoteByAsync(string personId, IList<VoteForDate> voteForDates)
        {
            throw new NotImplementedException();
        }

        public Task SubmitPlaceVoteByAsync(string personId, IList<VoteForPlace> voteForPlaces)
        {
            throw new NotImplementedException();
        }
    }
}