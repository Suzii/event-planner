using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Domain;
using PlaceEntity = EventPlanner.Entities.PlaceEntity;

namespace EventPlanner.DAL.Repository
{
    public class PlaceRepository
    {
        public async Task<List<PlaceEntity>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .ToList()
                    .Select(Mapper.Map<PlaceEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<PlaceEntity> GetPlace(Guid placeId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .FirstOrDefault(e => e.Id == placeId);

                return await Task.FromResult(Mapper.Map<PlaceEntity>(result));
            }
        }

        public async Task<List<PlaceEntity>> GetByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .Where(e => e.EventId == eventId)
                    .ToList()
                    .Select(Mapper.Map<PlaceEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<PlaceEntity> AddOrUpdate(PlaceEntity placeEntity)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<PlaceEntity>(placeEntity);

                context.Places.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<PlaceEntity>(entity);
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
