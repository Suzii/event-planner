using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class VotingService : IVotingService
    {
        public Task<IList<VoteForDateEntity>> GetVotesForDates(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForPlaceEntity>> GetVotesForPlaces(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForDateEntity>> GetVotesForDates(Guid eventId, IList<Guid> dateIds)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VoteForPlaceEntity>> GetVotesForPlaces(Guid eventId, IList<Guid> placeIds)
        {
            throw new NotImplementedException();
        }

        public Task SubmitDateVoteBy(Guid personId, IList<Guid> timeSlotIds)
        {
            throw new NotImplementedException();
        }

        public Task SubmitPlaceVoteBy(Guid personId, IList<Guid> placeIds)
        {
            throw new NotImplementedException();
        }
    }
}