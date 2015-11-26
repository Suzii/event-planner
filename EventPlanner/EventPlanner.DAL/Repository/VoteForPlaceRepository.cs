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
    /// <summary>
    ///     Repository class for place votes.
    ///     Handles all CRUD operations.
    /// </summary>
    public class VoteForPlaceRepository
    {
        /// <summary>
        ///     Asynchronous method that returns all existing place votes.
        /// </summary>
        /// <returns>List of place votes</returns>
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

        /// <summary>
        ///     Asynchronous method that returns place vote model with related user based on place vote entity Id.
        /// </summary>
        /// <param name="voteForPlaceId">Id of an place vote entity</param>
        /// <returns>Place vote model with related user.</returns>
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

        /// <summary>
        ///     Asynchronous method that returns place votes based on user Id
        /// </summary>
        /// <param name="userId">Id of an voter</param>
        /// <returns>List of place votes based on user Id</returns>
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

        /// <summary>
        ///     Asynchronous method that adds or update en existed place vote based on place vote entity.
        /// </summary>
        /// <param name="voteForPlace">Place vote entity with related</param>
        /// <returns>Returns created place vote entity</returns>
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

        /// <summary>
        ///     Asynchronous method that deletes place vote based on Id.
        /// </summary>
        /// <param name="eventId">Id of a place vote</param>
        /// <returns>True if successfully deleted or false if place vote does not exist</returns>
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
