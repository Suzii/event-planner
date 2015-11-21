using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IVotingService
    {
        Task<int> GetTotalNumberOfVotersForEvent(Guid eventId);

        Task<IList<Place>> GetPlacesWithVotes(Guid eventId);

        Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId);

        Task<IList<VoteForDate>> GetVotesForDatesAsync(Guid eventId);

        Task<IList<VoteForPlace>> GetVotesForPlacesAsync(Guid eventId);

        Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId);

        Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId);

        Task SubmitDateVotesByAsync(IList<VoteForDate> voteForDates);

        Task SubmitPlaceVotesByAsync(IList<VoteForPlace> voteForPlaces);

        Task<VoteForDate> SubmitVoteForDate(VoteForDate voteForDate);

        Task<VoteForPlace> SubmitVoteForPlace(VoteForPlace voteForPlace);
    }
}
