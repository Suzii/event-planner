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
    /// <summary>
    ///     Repository class for date votes.
    ///     Handles all CRUD operations.
    /// </summary>
    public class VoteForDateRepository
    {
        /// <summary>
        ///     Asynchronous method that returns all existing date votes.
        /// </summary>
        /// <returns>List of date votes</returns>
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

        /// <summary>
        ///     Asynchronous method that returns date vote model with related user based on date vote entity Id.
        /// </summary>
        /// <param name="voteForDateId">Id of an date vote entity</param>
        /// <returns>Date vote model with related user.</returns>
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

        /// <summary>
        ///     Asynchronous method that returns date votes based on user Id
        /// </summary>
        /// <param name="userId">Id of an voter</param>
        /// <returns>List of date votes based on user Id</returns>
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

        /// <summary>
        ///     Asynchronous method that adds or update en existed date vote based on date vote entity.
        /// </summary>
        /// <param name="voteForDate">Date vote entity with related</param>
        /// <returns>Returns created date vote entity</returns>
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

        /// <summary>
        ///     Asynchronous method that deletes date vote based on Id.
        /// </summary>
        /// <param name="voteForDateId">Id of a date vote</param>
        /// <returns>True if successfully deleted or false if date vote does not exist</returns>
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
