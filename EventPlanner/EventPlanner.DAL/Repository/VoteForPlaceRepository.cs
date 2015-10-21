using EventPlanner.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Domain;

namespace EventPlanner.DAL.Repository
{
    public class VoteForPlaceRepository
    {
        public async Task<List<VoteForPlace>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
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
                    .FirstOrDefault(e => e.Id == voteForPlaceId);

                return await Task.FromResult(Mapper.Map<VoteForPlace>(result));
            }
        }

        public async Task<List<VoteForPlace>> GetByUser(Guid userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForPlaces
                    .Where(e => e.UserId == userId)
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

        public static void CreateMap()
        {
            Mapper.CreateMap<VoteForPlace, VoteForPlaceEntity>()
                .ForMember(v => v.Id, conf => conf.MapFrom(ve => ve.Id))
                .ForMember(v => v.UserId, conf => conf.MapFrom(ve => ve.UserId))
                .ForMember(v => v.PlaceId, conf => conf.MapFrom(ve => ve.PlaceId));


            Mapper.CreateMap<VoteForPlaceEntity, VoteForPlace>()
                .ForMember(ve => ve.Id, conf => conf.MapFrom(v => v.Id))
                .ForMember(ve => ve.UserId, conf => conf.MapFrom(v => v.UserId))
                .ForMember(ve => ve.PlaceId, conf => conf.MapFrom(v => v.PlaceId));
        }
    }
}
