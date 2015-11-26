using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventDetailsService : IEventDetailsService
    {
        private readonly PlaceRepository _placeRepository;

        private readonly TimeSlotRepository _timeSlotRepository;

        public EventDetailsService() : this(new PlaceRepository(), new TimeSlotRepository())
        {
        }

        public EventDetailsService(PlaceRepository placeRepository, TimeSlotRepository timeSlotRepository)
        {
            _placeRepository = placeRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IList<Place>> GetPlaces(Guid eventId)
        {
            return await _placeRepository.GetPlaceInfoByEvent(eventId);
        }

        public async Task<IList<Place>> GetPlacesWithVotes(Guid eventId)
        {
            return await _placeRepository.GetPlaceWithVotesByEvent(eventId);
        }

        public async Task<IList<TimeSlot>> GetDates(Guid eventId)
        {
            return await _timeSlotRepository.GetTimeSlotInfoByEvent(eventId);
        }

        public async Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId)
        {
            return await _timeSlotRepository.GetTimeSlotInfoByEvent(eventId);
        }

    }
}
