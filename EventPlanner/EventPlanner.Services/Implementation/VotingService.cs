using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using System.Linq;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class providing services to operate with votes
    /// </summary>
    public class VotingService : IVotingService
    {
        private readonly VoteForDateRepository _voteForDateRepository;
        private readonly VoteForPlaceRepository _voteForPlaceRepository;
        private readonly IEventDetailsService _eventDetailsService;
        
        /// <summary>
        /// Initialize new instance of VotingService
        /// </summary>
        public VotingService() : this(new EventDetailsService(), new VoteForDateRepository(), new VoteForPlaceRepository())
        {

        }

        /// <summary>
        /// Initialize new instance of VotingService
        /// </summary>
        /// <param name="eventDetailsService">EventDetailsService to operate with</param>
        /// <param name="voteForDateRepository">VoteForDateRepository to operate with</param>
        /// <param name="voteForPlaceRepository">VoteForPlaceRepository to operate with</param>
        public VotingService(IEventDetailsService eventDetailsService, VoteForDateRepository voteForDateRepository,
            VoteForPlaceRepository voteForPlaceRepository)
        {
            _eventDetailsService = eventDetailsService;
            _voteForDateRepository = voteForDateRepository;
            _voteForPlaceRepository = voteForPlaceRepository;
        }

        /// <summary>
        /// Get voters count for specific Event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>Voters count</returns>
        public async Task<int> GetTotalNumberOfVotersForEvent(Guid eventId)
        {
            var places = await _eventDetailsService.GetPlacesWithVotes(eventId);
            var timeSlots = await _eventDetailsService.GetDatesWithVotes(eventId);
                        
            var placesTotalVoters = places.Where(p => p.VotesForPlace != null).SelectMany(p => p.VotesForPlace?.Select(v => v.UserId).ToList()).Distinct();
            var datesTotalVoters = timeSlots.Where(p => p.VotesForDate != null).SelectMany(p => p.VotesForDate?.Select(v => v.UserId).ToList()).Distinct();

            return placesTotalVoters.Union(datesTotalVoters).Distinct().Count();
        }

        /// <summary>
        /// Get all votes for specific date in specific event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="dateId">TimeSlot Id</param>
        /// <returns>List of Votes for Date</returns>
        public async Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId)
        {
            var allVotes= await _voteForDateRepository.GetAll();
            return allVotes.Where(v => v.TimeSlotId == dateId).ToList();

            //The above option seems to be more efficient as no join statement is needed in db
            //var timeSlots = await _timeSlotRepository.GetByEvent(eventId);
            //return timeSlots.Where(ts => ts.Id == dateId).SelectMany(ts => ts.VotesForDate).ToList();
        }

        /// <summary>
        /// Get all votes for specific place in specific event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="placeId">Place Id</param>
        /// <returns>List of Votes for Place</returns>
        public async Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId)
        {
            var allVotes = await _voteForPlaceRepository.GetAll();
            return allVotes.Where(v => v.PlaceId == placeId).ToList();

            //The above option seems to be more efficient as no join statement is needed in db
            //var places = await _placeRepository.GetByEvent(eventId);
            //return places.Where(p => p.Id == placeId).SelectMany(ts => ts.VotesForPlace).ToList();
        }

        /// <summary>
        /// Submit Vote for date and stores it to database
        /// </summary>
        /// <param name="voteForDate">VoteForDate to submit</param>
        /// <returns>Submitted VoteForDate</returns>
        public async Task<VoteForDate> SubmitVoteForDate(VoteForDate voteForDate)
        {
            return await _voteForDateRepository.AddOrUpdate(voteForDate);
        }

        /// <summary>
        /// Submit Vote for Place and stores it to database
        /// </summary>
        /// <param name="voteForPlace">VoteForPlace to submit</param>
        /// <returns>Submitted VoteForPlace</returns>
        public async Task<VoteForPlace> SubmitVoteForPlace(VoteForPlace voteForPlace)
        {
            return await _voteForPlaceRepository.AddOrUpdate(voteForPlace);
        }
    }
}