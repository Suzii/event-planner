using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;
using VoteForPlaceEntity = EventPlanner.Entities.VoteForPlaceEntity;

namespace EventPlanner.DAL.Repository
{
    public class VoteForPlaceRepository
    {
        public async Task<List<VoteForPlaceEntity>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .ToList()
                    .Select(Mapper.Map<VoteForPlaceEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForPlaceEntity> GetVoteForPlace(Guid voteForPlaceId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .FirstOrDefault(e => e.Id == voteForPlaceId);

                return await Task.FromResult(Mapper.Map<VoteForPlaceEntity>(result));
            }
        }

        public async Task<List<VoteForPlaceEntity>> GetByUser(Guid userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .Where(e => e.UserId == userId)
                    .ToList()
                    .Select(Mapper.Map<VoteForPlaceEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForPlaceEntity> AddOrUpdate(VoteForPlaceEntity voteForPlaceEntity)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<VoteForPlaceEntity>(voteForPlaceEntity);

                context.VotesForPlaces.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<VoteForPlaceEntity>(entity);
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
