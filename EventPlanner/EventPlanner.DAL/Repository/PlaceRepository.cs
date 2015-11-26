using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Domain;
using PlaceEntity = EventPlanner.Entities.PlaceEntity;

namespace EventPlanner.DAL.Repository
{
    /// <summary>
    ///     Repository class for place.
    ///     Handles all CRUD operations.
    /// </summary>
    public class PlaceRepository
    {
        /// <summary>
        ///     Asynchronous method that returns all existing places.
        /// </summary>
        /// <returns>List of places</returns>
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

        /// <summary>
        ///     Asynchronous method that returns an place model based on Id.
        /// </summary>
        /// <param name="eventId">Id of a place</param>
        /// <returns>Basic event model without related entities.</returns>
        public async Task<Place> GetPlace(Guid placeId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .FirstOrDefault(e => e.Id == placeId);

                return await Task.FromResult(Mapper.Map<Place>(result));
            }
        }

        /// <summary>
        ///     Asynchronous method that returns an basic places models based on event Id.
        /// </summary>
        /// <param name="eventId">Id of an event</param>
        /// <returns>List of basic events models without related votes entities.</returns>
        public async Task<List<Place>> GetPlaceInfoByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .Where(e => e.Event.Id == eventId)
                    .Select(Mapper.Map<Place>)
                    .ToList();

                return await Task.FromResult(result);
            }
        }

        
        public async Task<List<Place>> GetPlaceWithVotesByEvent(Guid eventId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.Places
                    .Where(e => e.Event.Id == eventId)
                    .Include("VotesForPlace")
                    .Include("VotesForPlace.User")
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

        public async Task<bool> Delete(Guid placeId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var existing = context.Places.FirstOrDefault(e => e.Id == placeId);

                if (existing == null)
                    return false;

                context.Places.Remove(existing);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
