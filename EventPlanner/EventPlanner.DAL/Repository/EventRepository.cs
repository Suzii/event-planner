using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using EventPlanner.Models.Domain;
using EventEntity = EventPlanner.Entities.EventEntity;

namespace EventPlanner.DAL.Repository
{
    public class EventRepository
    {
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

        public async Task<Event> GetEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .FirstOrDefault(e => e.Id == eventId);
                    
                return await Task.FromResult(Mapper.Map<Event>(result));
            }
        }

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

        public async Task<Event> AddOrUpdate(Event ev)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<EventEntity>(ev);

                context.Events.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<Event>(entity);
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
