using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class providing services to manage events
    /// </summary>
    public class EventManagementService : IEventManagementService
    {
        private readonly EventRepository _eventRepository;
        private readonly PlaceRepository _placeRepository;
        private readonly TimeSlotRepository _timeSlotRepository;

        /// <summary>
        /// Initialize new instance of EventManagementService
        /// </summary>
        public EventManagementService() : this(new EventRepository(), new PlaceRepository(), new TimeSlotRepository())
        {

        }

        /// <summary>
        /// Initialize new instance of EventManagementService
        /// </summary>
        /// <param name="eventRepository">EventRepository to operate with</param>
        /// <param name="placeRepository">PlaceRepository to operate with</param>
        /// <param name="timeSlotRepository">TomeSlotRepository to operate with</param>
        public EventManagementService(EventRepository eventRepository, PlaceRepository placeRepository,
            TimeSlotRepository timeSlotRepository)
        {
            _eventRepository = eventRepository;
            _placeRepository = placeRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        /// <summary>
        /// Create new event and stores it to database in async operation.
        /// </summary>
        /// <param name="e">Event to create. Event should not have filled Id</param>
        /// <param name="userId">Id of user who is creating this event</param>
        /// <returns>Created event with generated Id</returns>
        public async Task<Event> CreateEventAsync(Event e, string userId)
        {
            e.OrganizerId = userId;
            e.CreatedOn = DateTime.Now;
            return await _eventRepository.AddOrUpdate(e);
        }

        /// <summary>
        /// Update event and stores it to database in async operation. Places and TimeSlots which were removed from this Event are deleted from database with its votes 
        /// </summary>
        /// <param name="e">Event to update</param>
        /// <returns>Updated event</returns>
        public async Task<Event> UpdateEventAsync(Event e)
        {
            var fullEvent = await GetFullEventAsync(e.Id);
            e.CreatedOn = fullEvent.CreatedOn;
            e.OrganizerId = fullEvent.OrganizerId;

            foreach (var place in e.Places)
            {
                place.EventId = e.Id;
            }

            foreach (var timeSlot in e.TimeSlots)
            {
                timeSlot.EventId = e.Id;
            }

            var placeOrphans = fullEvent.Places.Select(pl => pl.Id).Except(e.Places.Select(p => p.Id)).ToList();
            var timeSlotOrphans = fullEvent.TimeSlots.Select(ts => ts.Id).Except(e.TimeSlots.Select(t => t.Id)).ToList();
            await DeleteEventPlaces(placeOrphans);
            await DeleteEventTimeSlots(timeSlotOrphans);
            return await _eventRepository.AddOrUpdate(e);
        }

        /// <summary>
        /// Disable event in async operation
        /// </summary>
        /// <param name="id">Event Id</param>
        public async Task DisableEventAsync(Guid id)
        {
            var e = await _eventRepository.GetEventInfo(id);
            e.Disabled = true;
            await _eventRepository.AddOrUpdate(e);
        }

        /// <summary>
        /// Get full event details with places and timeslots
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>Event with full details</returns>
        public async Task<Event> GetFullEventAsync(Guid id)
        {
            return await _eventRepository.GetFullEvent(id);
        }

        /// <summary>
        /// Get basic event details without places and timeslots
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>Basic event details without Places and Timeslots</returns>
        public async Task<Event> GetEventInfoAsync(Guid id)
        {
            return await _eventRepository.GetEventInfo(id);
        }

        /// <summary>
        /// Decode EventHash to its Id
        /// </summary>
        /// <param name="eventHash">Hash of an event</param>
        /// <returns>Id of event</returns>
        public Guid GetEventId(string eventHash)
        {
            eventHash = eventHash.Replace("_", "/");
            eventHash = eventHash.Replace("-", "+");
            var buffer = Convert.FromBase64String(eventHash + "==");
            return new Guid(buffer);
        }

        private async Task<bool> DeleteEventPlace(Guid placeId)
        {
            return await _placeRepository.Delete(placeId);
        }

        private async Task<bool> DeleteEventPlaces(IList<Guid> placeIds)
        {
            var deletedAll = true;
            foreach (var id in placeIds)
            {
                var d = await DeleteEventPlace(id);
                if (!d)
                {
                    deletedAll = false;
                }
            }
            return deletedAll;
        }

        private async Task<bool> DeleteEventTimeSlot(Guid timeSlotId)
        {
            return await _timeSlotRepository.Delete(timeSlotId);
        }

        private async Task<bool> DeleteEventTimeSlots(IList<Guid> timeSlotsIds)
        {
            var deletedAll = true;
            foreach (var id in timeSlotsIds)
            {
                var d = await DeleteEventTimeSlot(id);
                if (!d)
                {
                    deletedAll = false;
                }
            }
            return deletedAll;
        }
    }
}