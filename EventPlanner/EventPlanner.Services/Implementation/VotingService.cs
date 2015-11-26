using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using System.Linq;

namespace EventPlanner.Services.Implementation
{
    public class VotingService : IVotingService
    {
        private readonly VoteForDateRepository _voteForDateRepository;
        private readonly VoteForPlaceRepository _voteForPlaceRepository;
        private readonly PlaceRepository _placeRepository;
        private readonly TimeSlotRepository _timeSlotRepository;

        public VotingService()
        {
            _voteForDateRepository = new VoteForDateRepository();
            _voteForPlaceRepository = new VoteForPlaceRepository();
            _placeRepository = new PlaceRepository();
            _timeSlotRepository = new TimeSlotRepository();
        }

        public async Task<int> GetTotalNumberOfVotersForEvent(Guid eventId)
        {
            var places = await GetPlacesWithVotes(eventId);
            var timeSlots = await GetDatesWithVotes(eventId);
                        
            var placesTotalVoters = places.Where(p => p.VotesForPlace != null).SelectMany(p => p.VotesForPlace?.Select(v => v.UserId).ToList()).Distinct();
            var datesTotalVoters = timeSlots.Where(p => p.VotesForDate != null).SelectMany(p => p.VotesForDate?.Select(v => v.UserId).ToList()).Distinct();

            return placesTotalVoters.Union(datesTotalVoters).Distinct().Count();
        }

        public async Task<IList<Place>> GetPlacesWithVotes(Guid eventId)
        {
            return await _placeRepository.GetByEvent(eventId);
        }

        public async Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId)
        {
            return await _timeSlotRepository.GetByEvent(eventId);   
        }

        public async Task<IList<VoteForDate>> GetVotesForDateAsync(Guid eventId, Guid dateId)
        {
            var allVotes= await _voteForDateRepository.GetAll();
            return allVotes.Where(v => v.TimeSlotId == dateId).ToList();

            //The above option seems to be more efficient as no join statement is needed in db
            //var timeSlots = await _timeSlotRepository.GetByEvent(eventId);
            //return timeSlots.Where(ts => ts.Id == dateId).SelectMany(ts => ts.VotesForDate).ToList();
        }

        public async Task<IList<VoteForPlace>> GetVotesForPlaceAsync(Guid eventId, Guid placeId)
        {
            var allVotes = await _voteForPlaceRepository.GetAll();
            return allVotes.Where(v => v.PlaceId == placeId).ToList();

            //The above option seems to be more efficient as no join statement is needed in db
            //var places = await _placeRepository.GetByEvent(eventId);
            //return places.Where(p => p.Id == placeId).SelectMany(ts => ts.VotesForPlace).ToList();
        }

        public async Task<VoteForDate> SubmitVoteForDate(VoteForDate voteForDate)
        {
            return await _voteForDateRepository.AddOrUpdate(voteForDate);
        }

        public async Task<VoteForPlace> SubmitVoteForPlace(VoteForPlace voteForPlace)
        {
            return await _voteForPlaceRepository.AddOrUpdate(voteForPlace);
        }
    }
}