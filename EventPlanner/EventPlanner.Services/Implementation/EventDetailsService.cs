using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class providing services to operate with Event Details
    /// </summary>
    public class EventDetailsService : IEventDetailsService
    {
        private readonly PlaceRepository _placeRepository;
        private readonly TimeSlotRepository _timeSlotRepository;

        /// <summary>
        /// Initialize new instance of EventDetailsService
        /// </summary>
        public EventDetailsService() : this(new PlaceRepository(), new TimeSlotRepository())
        {

        }

        /// <summary>
        /// Initialize new instance of EventDetailsService
        /// </summary>
        /// <param name="placeRepository">PlaceRepository to operate with</param>
        /// <param name="timeSlotRepository">TimeSlotRepository to operate with</param>
        public EventDetailsService(PlaceRepository placeRepository, TimeSlotRepository timeSlotRepository)
        {
            _placeRepository = placeRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        /// <summary>
        /// Get all places without its votes for given event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>List of places without votes</returns>
        public async Task<IList<Place>> GetPlaces(Guid eventId)
        {
            return await _placeRepository.GetPlaceInfoByEvent(eventId);
        }

        /// <summary>
        /// Get all places with its votes for given event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>List of places with votes</returns>
        public async Task<IList<Place>> GetPlacesWithVotes(Guid eventId)
        {
            return await _placeRepository.GetPlaceWithVotesByEvent(eventId);
        }

        /// <summary>
        /// Get all TimeSlots without its votes for given event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>List of TimeSlots without votes</returns>
        public async Task<IList<TimeSlot>> GetDates(Guid eventId)
        {
            return await _timeSlotRepository.GetTimeSlotInfoByEvent(eventId);
        }

        /// <summary>
        /// Get all TimeSlots with its votes for given event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>List of TimeSlots with votes</returns>
        public async Task<IList<TimeSlot>> GetDatesWithVotes(Guid eventId)
        {
            return await _timeSlotRepository.GetTimeSlotInfoByEvent(eventId);
        }

    }
}
