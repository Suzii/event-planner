using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;
using VoteForDateEntity = EventPlanner.Entities.VoteForDateEntity;

namespace EventPlanner.DAL.Repository
{
    public class VoteForDateRepository
    {
        public async Task<List<VoteForDateEntity>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .ToList()
                    .Select(Mapper.Map<VoteForDateEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForDateEntity> GetVoteForDate(Guid voteForDateId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .FirstOrDefault(e => e.Id == voteForDateId);

                return await Task.FromResult(Mapper.Map<VoteForDateEntity>(result));
            }
        }

        public async Task<List<VoteForDateEntity>> GetByUser(Guid userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .Where(e => e.UserId == userId)
                    .ToList()
                    .Select(Mapper.Map<VoteForDateEntity>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForDateEntity> AddOrUpdate(VoteForDateEntity voteForDateEntity)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<VoteForDateEntity>(voteForDateEntity);

                context.VotesForDates.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<VoteForDateEntity>(entity);
            }
        }

        public async Task<bool> Delete(Guid voteForDateId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var existing = context.VotesForDates.FirstOrDefault(e => e.Id == voteForDateId);

                if (existing == null)
                    return false;

                context.VotesForDates.Remove(existing);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
