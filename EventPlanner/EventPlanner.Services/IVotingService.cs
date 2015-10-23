using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    interface IVotingService
    {
        Task<IList<VoteForDateEntity>> GetVotesForDates(Guid eventId);

        Task<IList<VoteForPlaceEntity>> GetVotesForPlaces(Guid eventId);

        Task<IList<VoteForDateEntity>> GetVotesForDates(Guid eventId, IList<Guid> dateIds);

        Task<IList<VoteForPlaceEntity>> GetVotesForPlaces(Guid eventId, IList<Guid> placeIds);

        Task SubmitDateVoteBy(Guid personId, IList<Guid> timeSlotIds);

        Task SubmitPlaceVoteBy(Guid personId, IList<Guid> placeIds);
    }
}
