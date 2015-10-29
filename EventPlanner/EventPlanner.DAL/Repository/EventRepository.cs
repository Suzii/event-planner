using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using EventEntity = EventPlanner.Entities.EventEntity;

namespace EventPlanner.DAL.Repository
{
    public class EventRepository
    {
        public async Task<List<EventEntity>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .ToList()
                    .Select(Mapper.Map<EventEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<EventEntity> GetEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .FirstOrDefault(e => e.Id == eventId);
                    
                return await Task.FromResult(Mapper.Map<EventEntity>(result));
            }
        }

        public async Task<List<EventEntity>> GetByOrganizer(Guid organizerId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .Where(e => e.OrganizerId == organizerId)
                    .ToList()
                    .Select(Mapper.Map<EventEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<EventEntity> AddOrUpdate(EventEntity eventEntity)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<EventEntity>(eventEntity);

                context.Events.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<EventEntity>(entity);
            }
        }

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
