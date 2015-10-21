using EventPlanner.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Domain;

namespace EventPlanner.DAL.Repository
{
    public class PlaceRepository
    {
        public async Task<List<Place>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .ToList()
                    .Select(Mapper.Map<Place>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<Place> GetPlace(Guid placeId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .FirstOrDefault(e => e.Id == placeId);

                return await Task.FromResult(Mapper.Map<Place>(result));
            }
        }

        public async Task<List<Place>> GetByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .Where(e => e.EventId == eventId)
                    .ToList()
                    .Select(Mapper.Map<Place>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<Place> AddOrUpdate(Place place)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<PlaceEntity>(place);

                context.Places.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<Place>(entity);
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
            Mapper.CreateMap<Place, PlaceEntity>()
                .ForMember(pe => pe.Id, conf => conf.MapFrom(p => p.Id))
                .ForMember(pe => pe.EventId, conf => conf.MapFrom(p => p.EventId))
                .ForMember(pe => pe.VenueId, conf => conf.MapFrom(p => p.VenueId))
                .ForMember(pe => pe.VotesForPlace, conf => conf.MapFrom(p => p.VotesForPlace));


            Mapper.CreateMap<PlaceEntity, Place>()
                .ForMember(p => p.Id, conf => conf.MapFrom(pe => pe.Id))
                .ForMember(p => p.EventId, conf => conf.MapFrom(pe => pe.EventId))
                .ForMember(p => p.VenueId, conf => conf.MapFrom(pe => pe.VenueId))
                .ForMember(p => p.VotesForPlace, conf => conf.MapFrom(pe => pe.VotesForPlace));
        }
    }
}
