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
        public async Task<List<VoteForPlace>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .Include("User")
                    .ToList()
                    .Select(Mapper.Map<VoteForPlace>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForPlace> GetVoteForPlace(Guid voteForPlaceId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .Include("User")
                    .FirstOrDefault(e => e.Id == voteForPlaceId);

                return await Task.FromResult(Mapper.Map<VoteForPlace>(result));
            }
        }

        public async Task<List<VoteForPlace>> GetByUser(string userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .Where(e => e.User.Id == userId)
                    .ToList()
                    .Select(Mapper.Map<VoteForPlace>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForPlace> AddOrUpdate(VoteForPlace voteForPlace)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<VoteForPlaceEntity>(voteForPlace);

                context.VotesForPlaces.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<VoteForPlace>(entity);
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
