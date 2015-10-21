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
    public class VoteForDateRepository
    {
        public async Task<List<VoteForDate>> GetAll()
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
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
                    .FirstOrDefault(e => e.Id == voteForDateId);

                return await Task.FromResult(Mapper.Map<VoteForDate>(result));
            }
        }

        public async Task<List<VoteForDate>> GetByUser(Guid userId)
        {
            using (var context = EventPlannerContext.Get())
            {
                var result = context.VotesForDates
                    .Where(e => e.UserId == userId)
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

        public static void CreateMap()
        {
            Mapper.CreateMap<VoteForDate, VoteForDateEntity>()
                .ForMember(v => v.Id, conf => conf.MapFrom(ve => ve.Id))
                .ForMember(v => v.UserId, conf => conf.MapFrom(ve => ve.UserId))
                .ForMember(v => v.TimeSlotId, conf => conf.MapFrom(ve => ve.TimeSlotId));

            Mapper.CreateMap<VoteForDateEntity, VoteForDate>()
                .ForMember(ve => ve.Id, conf => conf.MapFrom(v => v.Id))
                .ForMember(ve => ve.UserId, conf => conf.MapFrom(v => v.UserId))
                .ForMember(ve => ve.TimeSlotId, conf => conf.MapFrom(v => v.TimeSlotId));
        }
    }
}
