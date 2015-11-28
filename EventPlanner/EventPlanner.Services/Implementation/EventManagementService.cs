using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class EventManagementService : IEventManagementService
    {
        private readonly EventRepository _eventRepository;
        private readonly PlaceRepository _placeRepository;
        private readonly TimeSlotRepository _timeSlotRepository;

        public EventManagementService()
        {
            _eventRepository = new EventRepository();
            _placeRepository = new PlaceRepository();
            _timeSlotRepository = new TimeSlotRepository();
        }

        public async Task<Event> CreateEventAsync(Event e, string userId)
        {
            e.OrganizerId = userId;
            e.CreatedOn = DateTime.Now;
            return await _eventRepository.AddOrUpdate(e);
        }

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

        public async Task DisableEventAsync(Guid id)
        {
            var e = await _eventRepository.GetEventInfo(id);
            e.Disabled = true;
            await _eventRepository.AddOrUpdate(e);
        }

        public async Task<Event> GetFullEventAsync(Guid id)
        {
            return await _eventRepository.GetFullEvent(id);
        }

        public async Task<Event> GetEventInfoAsync(Guid id)
        {
            return await _eventRepository.GetEventInfo(id);
        }

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