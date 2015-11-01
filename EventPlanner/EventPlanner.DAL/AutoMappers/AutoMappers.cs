using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;

namespace EventPlanner.DAL.AutoMappers
{
    /// <summary>
    /// Automappers for all repository classes
    /// </summary>
    public static class AutoMappers
    {
        public static void Configure()
        {
            CreateEventMap();
            CreatePlaceMap();
            CreateTimeSlotMap();
            CreateVoteForDateMap();
            CreateVoteForPlaceMap();
        }

        private static void CreateEventMap()
        {
            Mapper.CreateMap<Models.Domain.Event, EventEntity>()
                .ForMember(ee => ee.Id, conf => conf.MapFrom(e => e.Id))
                .ForMember(ee => ee.OrganizerId, conf => conf.MapFrom(e => e.OrganizerId))
                .ForMember(ee => ee.OthersCanEdit, conf => conf.MapFrom(e => e.OthersCanEdit))
                //.ForMember(ee => ee.Places, conf => conf.MapFrom(e => e.Places))
                //.ForMember(ee => ee.TimeSlots, conf => conf.MapFrom(e => e.TimeSlots))
                .ForMember(ee => ee.Title, conf => conf.MapFrom(e => e.Title))
                .ForMember(ee => ee.Hash, conf => conf.MapFrom(e => e.Hash))
                .ForMember(ee => ee.ExpectedLength, conf => conf.MapFrom(e => e.ExpectedLength))
                .ForMember(ee => ee.Created, conf => conf.MapFrom(e => e.Created))
                .ForMember(ee => ee.Disabled, conf => conf.MapFrom(e => e.Disabled))
                .ForMember(ee => ee.Desc, conf => conf.MapFrom(e => e.Desc));
            
            Mapper.CreateMap<EventEntity, Models.Domain.Event>()
                .ForMember(e => e.Id, conf => conf.MapFrom(ee => ee.Id))
                .ForMember(e => e.OthersCanEdit, conf => conf.MapFrom(ee => ee.OthersCanEdit))
                .ForMember(e => e.OrganizerId, conf => conf.MapFrom(ee => ee.OrganizerId))
                //.ForMember(e => e.Places, conf => conf.ResolveUsing(ee => ee.Places.ToList()))
                //.ForMember(e => e.TimeSlots, conf => conf.ResolveUsing(ee => ee.TimeSlots.ToList()))
                .ForMember(e => e.Title, conf => conf.MapFrom(ee => ee.Title))
                .ForMember(e => e.Hash, conf => conf.MapFrom(ee => ee.Hash))
                .ForMember(e => e.ExpectedLength, conf => conf.MapFrom(ee => ee.ExpectedLength))
                .ForMember(e => e.Created, conf => conf.MapFrom(ee => ee.Created))
                .ForMember(e => e.Disabled, conf => conf.MapFrom(ee => ee.Disabled))
                .ForMember(e => e.Desc, conf => conf.MapFrom(ee => ee.Desc));
        }

        private static void CreatePlaceMap()
        {
            Mapper.CreateMap<Models.Domain.Place, PlaceEntity>()
                .ForMember(pe => pe.Id, conf => conf.MapFrom(p => p.Id))
                .ForMember(pe => pe.EventId, conf => conf.MapFrom(p => p.EventId))
                .ForMember(pe => pe.VenueId, conf => conf.MapFrom(p => p.VenueId));
                //.ForMember(pe => pe.VotesForPlace, conf => conf.MapFrom(p => p.VotesForPlace));

            Mapper.CreateMap<PlaceEntity, Models.Domain.Place>()
                .ForMember(p => p.Id, conf => conf.MapFrom(pe => pe.Id))
                .ForMember(p => p.EventId, conf => conf.MapFrom(pe => pe.EventId))
                .ForMember(p => p.VenueId, conf => conf.MapFrom(pe => pe.VenueId));
            //.ForMember(p => p.VotesForPlace, conf => conf.MapFrom(pe => pe.VotesForPlace));
        }

        private static void CreateTimeSlotMap()
        {
            Mapper.CreateMap<TimeSlotEntity, Models.Domain.TimeSlot>()
                .ForMember(t => t.Id, conf => conf.MapFrom(te => te.Id))
                .ForMember(t => t.EventId, conf => conf.MapFrom(te => te.EventId))
                .ForMember(t => t.DateTime, conf => conf.MapFrom(te => te.DateTime));
                //.ForMember(t => t.VotesForDate, conf => conf.ResolveUsing(te => te.VotesForDate.ToList()));


            Mapper.CreateMap<Models.Domain.TimeSlot, TimeSlotEntity>()
                .ForMember(te => te.Id, conf => conf.MapFrom(t => t.Id))
                .ForMember(te => te.EventId, conf => conf.MapFrom(t => t.EventId))
                .ForMember(te => te.DateTime, conf => conf.MapFrom(t => t.DateTime));
                //.ForMember(te => te.VotesForDate, conf => conf.MapFrom(t => t.VotesForDate));
        }

        private static void CreateVoteForDateMap()
        {
            Mapper.CreateMap<Models.Domain.VoteForDate, VoteForDateEntity>()
                .ForMember(v => v.Id, conf => conf.MapFrom(ve => ve.Id))
                .ForMember(v => v.UserId, conf => conf.MapFrom(ve => ve.UserId))
                .ForMember(v => v.TimeSlotId, conf => conf.MapFrom(ve => ve.TimeSlotId));

            Mapper.CreateMap<VoteForDateEntity, Models.Domain.VoteForDate>()
                .ForMember(ve => ve.Id, conf => conf.MapFrom(v => v.Id))
                .ForMember(ve => ve.UserId, conf => conf.MapFrom(v => v.UserId))
                .ForMember(ve => ve.TimeSlotId, conf => conf.MapFrom(v => v.TimeSlotId));
        }

        private static void CreateVoteForPlaceMap()
        {
            Mapper.CreateMap<VoteForPlaceEntity, Models.Domain.VoteForPlace>()
                .ForMember(v => v.Id, conf => conf.MapFrom(ve => ve.Id))
                .ForMember(v => v.UserId, conf => conf.MapFrom(ve => ve.UserId))
                .ForMember(v => v.PlaceId, conf => conf.MapFrom(ve => ve.PlaceId));


            Mapper.CreateMap<Models.Domain.VoteForPlace, VoteForPlaceEntity>()
                .ForMember(ve => ve.Id, conf => conf.MapFrom(v => v.Id))
                .ForMember(ve => ve.UserId, conf => conf.MapFrom(v => v.UserId))
                .ForMember(ve => ve.PlaceId, conf => conf.MapFrom(v => v.PlaceId));
        }
       
    }
}
