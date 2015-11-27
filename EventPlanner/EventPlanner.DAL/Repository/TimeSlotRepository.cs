using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;
using TimeSlotEntity = EventPlanner.Entities.TimeSlotEntity;

namespace EventPlanner.DAL.Repository
{
    /// <summary>
    ///     Repository class for time and date (timeslot).
    ///     Handles all CRUD operations.
    /// </summary>
    public class TimeSlotRepository
    {
        /// <summary>
        ///     Asynchronous method that returns all existing timeslots.
        /// </summary>
        /// <returns>List of timeslots</returns>
        public async Task<List<TimeSlot>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .ToList()
                    .Select(Mapper.Map<TimeSlot>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        /// <summary>
        ///     Asynchronous method that returns a timeslot model based on Id.
        /// </summary>
        /// <param name="timeSlotId">Id of a timeslot</param>
        /// <returns>Basic timeslot model without related entities.</returns>
        public async Task<TimeSlot> GetTimeSlot(Guid timeSlotId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .FirstOrDefault(e => e.Id == timeSlotId);

                return await Task.FromResult(Mapper.Map<TimeSlot>(result));
            }
        }

        /// <summary>
        ///     Asynchronous method that returns basic timeslots models based on event Id.
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>List of basic timeslot models without related votes entities.</returns>
        public async Task<List<TimeSlot>> GetTimeSlotInfoByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .Where(e => e.EventId == eventId)
                    .Select(Mapper.Map<TimeSlot>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        /// <summary>
        ///     Asynchronous method that returns a timeslot models with related entities based on event Id.
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>List of timeslot models with related votes entities.</returns>
        public async Task<List<TimeSlot>> GetTimeSlotWithVotesByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .Where(e => e.EventId == eventId)
                    .Include("VotesForDate")
                    .Include("VotesForDate.User")
                    .Select(Mapper.Map<TimeSlot>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        /// <summary>
        ///     Asynchronous method that adds or update existed timeslot based on place entity.
        /// </summary>
        /// <param name="timeSlot">Timeslot entity</param>
        /// <returns>Returns created timeslot entity</returns>
        public async Task<TimeSlot> AddOrUpdate(TimeSlot timeSlot)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<TimeSlotEntity>(timeSlot);

                context.TimeSlots.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<TimeSlot>(entity);
            }
        }

        /// <summary>
        ///     Asynchronous method that deletes a timeslot based on Id.
        /// </summary>
        /// <param name="timeSlotId">Id of a timeslot</param>
        /// <returns>True if successfully deleted or false if timeslot does not exist</returns>
        public async Task<bool> Delete(Guid timeSlotId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var existing = context.TimeSlots.FirstOrDefault(e => e.Id == timeSlotId);

                if (existing == null)
                    return false;

                context.TimeSlots.Remove(existing);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
