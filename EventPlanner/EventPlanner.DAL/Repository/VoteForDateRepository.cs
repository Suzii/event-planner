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
        public async Task<List<VoteForDate>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .Include("User")
                    .ToList()
                    .Select(Mapper.Map<VoteForDate>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForDate> GetVoteForDate(Guid voteForDateId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .Include("User")
                    .FirstOrDefault(e => e.Id == voteForDateId);

                return await Task.FromResult(Mapper.Map<VoteForDate>(result));
            }
        }

        public async Task<List<VoteForDate>> GetByUser(string userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .Where(e => e.User.Id == userId)
                    .ToList()
                    .Select(Mapper.Map<VoteForDate>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        public async Task<VoteForDate> AddOrUpdate(VoteForDate voteForDate)
        {
            using (var context = EventPlannerContext.Get())
            {
                var entity = Mapper.Map<VoteForDateEntity>(voteForDate);

                context.VotesForDates.AddOrUpdate(entity);
                await context.SaveChangesAsync();

                return Mapper.Map<VoteForDate>(entity);
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
