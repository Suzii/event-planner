using EventPlanner.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Domain;

namespace EventPlanner.DAL.Repository
{
    public class TimeSlotRepository
    {
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

        public async Task<TimeSlot> GetTimeSlot(Guid timeSlotId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .FirstOrDefault(e => e.Id == timeSlotId);

                return await Task.FromResult(Mapper.Map<TimeSlot>(result));
            }
        }

        public async Task<List<TimeSlot>> GetByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.TimeSlots
                    .Where(e => e.EventId == eventId)
                    .ToList()
                    .Select(Mapper.Map<TimeSlot>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

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

        public static void CreateMap()
        {
            Mapper.CreateMap<TimeSlot, TimeSlotEntity>()
                .ForMember(t => t.Id, conf => conf.MapFrom(te => te.Id))
                .ForMember(t => t.EventId, conf => conf.MapFrom(te => te.EventId))
                .ForMember(t => t.DateTime, conf => conf.MapFrom(te => te.DateTime))
                .ForMember(t => t.VotesForDate, conf => conf.MapFrom(te => te.VotesForDate));


            Mapper.CreateMap<TimeSlotEntity, TimeSlot>()
                .ForMember(te => te.Id, conf => conf.MapFrom(t => t.Id))
                .ForMember(te => te.EventId, conf => conf.MapFrom(t => t.EventId))
                .ForMember(te => te.DateTime, conf => conf.MapFrom(t => t.DateTime))
                .ForMember(te => te.VotesForDate, conf => conf.MapFrom(t => t.VotesForDate));
        }
    }
}
