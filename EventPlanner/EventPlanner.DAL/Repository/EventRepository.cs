using EventPlanner.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;
using System.Data.Entity.Migrations;

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

        public async Task<List<Event>> GetByOrganizer(Guid organizerId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Events
                    .Where(e => e.OrganizerId == organizerId)
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

        public static void CreateMap()
        {
            Mapper.CreateMap<Event, EventEntity>()
                .ForMember(ee => ee.Id, conf => conf.MapFrom(e => e.Id))
                .ForMember(ee => ee.OrganizerId, conf => conf.MapFrom(e => e.OrganizerId))
                .ForMember(ee => ee.OthersCanEdit, conf => conf.MapFrom(e => e.OthersCanEdit))
                .ForMember(ee => ee.Places, conf => conf.MapFrom(e => e.Places))
                .ForMember(ee => ee.TimeSlots, conf => conf.MapFrom(e => e.TimeSlots))
                .ForMember(ee => ee.Title, conf => conf.MapFrom(e => e.Title))
                .ForMember(ee => ee.Hash, conf => conf.MapFrom(e => e.Hash))
                .ForMember(ee => ee.ExpectedLength, conf => conf.MapFrom(e => e.ExpectedLength))
                .ForMember(ee => ee.Created, conf => conf.MapFrom(e => e.Created))
                .ForMember(ee => ee.Desc, conf => conf.MapFrom(e => e.Desc));


            Mapper.CreateMap<EventEntity, Event>()
                .ForMember(e => e.Id, conf => conf.MapFrom(ee => ee.Id))
                .ForMember(e => e.OthersCanEdit, conf => conf.MapFrom(ee => ee.OthersCanEdit))
                .ForMember(e => e.OrganizerId, conf => conf.MapFrom(ee => ee.OrganizerId))
                .ForMember(e => e.Places, conf => conf.ResolveUsing(ee => ee.Places.ToList()))
                .ForMember(e => e.TimeSlots, conf => conf.ResolveUsing(ee => ee.TimeSlots.ToList()))
                .ForMember(e => e.Title, conf => conf.MapFrom(ee => ee.Title))
                .ForMember(e => e.Hash, conf => conf.MapFrom(ee => ee.Hash))
                .ForMember(e => e.ExpectedLength, conf => conf.MapFrom(ee => ee.ExpectedLength))
                .ForMember(e => e.Created, conf => conf.MapFrom(ee => ee.Created))
                .ForMember(e => e.Desc, conf => conf.MapFrom(ee => ee.Desc));
        }
    }
}
