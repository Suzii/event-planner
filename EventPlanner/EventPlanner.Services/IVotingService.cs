using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IVotingService
    {
        //Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId);

        //Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId);

        //Task<IList<VoteForDateEntity>> GetVotesForDatesAsync(Guid eventId, IList<Guid> dateIds);

        //Task<IList<VoteForPlaceEntity>> GetVotesForPlacesAsync(Guid eventId, IList<Guid> placeIds);

        Task SubmitDateVoteByAsync(string personId, IList<VoteForDate> voteForDates);

        Task SubmitPlaceVoteByAsync(string personId, IList<VoteForPlace> voteForPlaces);
    }
}
