using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;
using TimeSlotEntity = EventPlanner.Entities.TimeSlotEntity;

namespace EventPlanner.DAL.Repository
{
    public class TimeSlotRepository
    {
        public async Task<List<TimeSlotEntity>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .ToList()
                    .Select(Mapper.Map<TimeSlotEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<TimeSlotEntity> GetTimeSlot(Guid timeSlotId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .FirstOrDefault(e => e.Id == timeSlotId);

                return await Task.FromResult(Mapper.Map<TimeSlotEntity>(result));
            }
        }

        public async Task<List<TimeSlotEntity>> GetByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .Where(e => e.EventId == eventId)
                    .ToList()
                    .Select(Mapper.Map<TimeSlotEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<TimeSlotEntity> AddOrUpdate(TimeSlotEntity timeSlotEntity)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<TimeSlotEntity>(timeSlotEntity);

                context.TimeSlots.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<TimeSlotEntity>(entity);
            }
        }

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
