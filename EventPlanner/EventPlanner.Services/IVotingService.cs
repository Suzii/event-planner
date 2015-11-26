using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// Interface containings methods for voting 
    /// </summary>
    public interface IVotingService
    {
        /// <summary>
        /// Get number of voters for event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>Number of voters</returns>
        Task<int> GetTotalNumberOfVotersForEvent(Guid eventId);

        /// <summary>
        /// Get places for event with votes for this place
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of places for event</returns>
        Task<IList<Place>> GetPlacesWithVotes(Guid eventId);

        /// <summary>
        /// Get dates for event with votes for this date
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <returns>List of dates with votes</returns>
        Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId);

        /// <summary>
        /// Get votes for specific date of specific event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="dateId">Date Id</param>
        /// <returns></returns>
        Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId);

        /// <summary>
        /// Get votes for specific place of specific event
        /// </summary>
        /// <param name="eventId">Event id</param>
        /// <param name="placeId">Place id</param>
        /// <returns></returns>
        Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId);

        /// <summary>
        /// Submit vote for date
        /// </summary>
        /// <param name="voteForDate">Vote to submit and store to db</param>
        /// <returns>Created vote for date</returns>
        Task<VoteForDate> SubmitVoteForDate(VoteForDate voteForDate);

        /// <summary>
        /// Submit vote for place
        /// </summary>
        /// <param name="voteForPlace">Vote to submit and store to db</param>
        /// <returns>Created vote for place</returns>
        Task<VoteForPlace> SubmitVoteForPlace(VoteForPlace voteForPlace);
    }
}
