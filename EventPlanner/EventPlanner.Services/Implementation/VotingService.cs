using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Services.Implementation
{
    public class VotingService : IVotingService
    {
        public Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId, IList<Guid> dateIds)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId, IList<Guid> placeIds)
        {
            throw new NotImplementedException();
        }

        public Task SubmitDateVoteByAsync(Guid personId, IList<Guid> timeSlotIds)
        {
            throw new NotImplementedException();
        }

        public Task SubmitPlaceVoteByAsync(Guid personId, IList<Guid> placeIds)
        {
            throw new NotImplementedException();
        }
    }
}