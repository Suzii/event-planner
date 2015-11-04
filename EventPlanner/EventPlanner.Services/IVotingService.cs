using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Services
{
    public interface IVotingService
    {
        Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId);

        Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId);

        Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId, IList<Guid> dateIds);

        Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId, IList<Guid> placeIds);

        Task SubmitDateVoteByAsync(string personId, IList<Guid> timeSlotIds);

        Task SubmitPlaceVoteByAsync(string personId, IList<Guid> placeIds);
    }
}
