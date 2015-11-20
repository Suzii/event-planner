using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IVotingService
    {
        Task<int> GetTotalNumberOfVotersOfEvent(Guid eventId);
        Task<IList<VoteForDate>> GetVotesForDatesAsync(Guid eventId);

        Task<IList<VoteForPlace>> GetVotesForPlacesAsync(Guid eventId);

        Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId);

        Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId);

        Task SubmitDateVotesByAsync(string personId, IList<VoteForDate> voteForDates);

        Task SubmitPlaceVotesByAsync(string personId, IList<VoteForPlace> voteForPlaces);

        Task SubmitVoteForDate(VoteForDate voteForDate);

        Task SubmitVoteForPlace(VoteForPlace voteForPlace);
    }
}
