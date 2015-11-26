using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using EventPlanner.Models.Domain;
using EventEntity = EventPlanner.Entities.EventEntity;

namespace EventPlanner.DAL.Repository
{
    /// <summary>
    ///     Repository class for event.
    ///     Handles all CRUD operations.
    /// </summary>
    public class EventRepository
    {
        /// <summary>
        ///     Asynchronous method that returns all existing events.
        /// </summary>
        /// <returns>List of events</returns>
        public async Task<List<Event>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .ToList()
                    .Select(Mapper.Map<Event>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        /// <summary>
        ///     Asynchronous method that returns an basic event model based on Id.
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>Basic event model without related entities.</returns>
        public async Task<Event> GetEventInfo(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .FirstOrDefault(e => e.Id == eventId);
                    
                return await Task.FromResult(Mapper.Map<Event>(result));
            }
        }
        
        /// <summary>
        ///     Returns an event with its places and timeslots based on Id
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>Event model with related places and timeslots.</returns>
        public async Task<Event> GetFullEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .Include("Places")
                    .Include("TimeSlots")
                    .FirstOrDefault(e => e.Id == eventId);

                return await Task.FromResult(Mapper.Map<Event>(result));
            }
        }

        /// <summary>
        ///     Asynchronous method that returns events based on organizer Id
        /// </summary>
        /// <param name="organizerId">Id of an organizer of an event</param>
        /// <returns>List of events based on organizer Id</returns>
        public async Task<List<Event>> GetByOrganizer(string organizerId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .Where(e => e.Organizer.Id == organizerId)
                    .ToList()
                    .Select(Mapper.Map<Event>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        /// <summary>
        ///     Asynchronous method that adds or update en existed event based on event entity.
        /// </summary>
        /// <param name="ev">Event entity with related timeslots and places</param>
        /// <returns>Returns created event entity</returns>
        public async Task<Event> AddOrUpdate(Event ev)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<EventEntity>(ev);

                foreach (var t in entity.TimeSlots)
                {
                    context.Entry(t).State = (t.Id == default(Guid) ? EntityState.Added : EntityState.Modified);
                }

                foreach (var p in entity.Places)
                {
                    context.Entry(p).State = (p.Id == default(Guid) ? EntityState.Added : EntityState.Modified);
                }

                context.Events.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<Event>(entity);
            }
        }

        /// <summary>
        ///     Asynchronous method that deletes an event based on Id.
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>True if successfully deleted or false if event does not exist</returns>
        public async Task<bool> Delete(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var existing = context.Events.FirstOrDefault(e => e.Id == eventId);

                if (existing == null)
                    return false;
                
                context.Events.Remove(existing);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
