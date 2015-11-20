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

        public async Task<Event> GetEventInfo(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .FirstOrDefault(e => e.Id == eventId);
                    
                return await Task.FromResult(Mapper.Map<Event>(result));
            }
        }

        public async Task<Event> GetFullEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .Include("Places")
                    .Include("TimeSlots")
                    //TODO: delete these two once service for Voting can handle it ???
                    .Include("Places.VotesForPlace")
                    .Include("TimeSlots.VotesForDate")
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
