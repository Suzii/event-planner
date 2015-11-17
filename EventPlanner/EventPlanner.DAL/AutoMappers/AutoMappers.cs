using System;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models.Enums;

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
            CreateEnumsMap();
            CreateUserMap();
        }

        private static void CreateEventMap()
        {
            Mapper.CreateMap<Models.Domain.Event, EventEntity>()
                .ForMember(ee => ee.Places, conf => conf.MapFrom(e => e.Places))
                .ForMember(ee => ee.TimeSlots, conf => conf.MapFrom(e => e.TimeSlots));

            Mapper.CreateMap<EventEntity, Models.Domain.Event>()
                .ForMember(e => e.Places, conf => conf.ResolveUsing(ee => ee.Places))
                .ForMember(e => e.TimeSlots, conf => conf.ResolveUsing(ee => ee.TimeSlots));

            Mapper.CreateMap<Models.Domain.Event, Models.Models.EventModel>()
                .ForMember(e => e.Dates, conf => conf.Ignore());

            Mapper.CreateMap<Models.Models.EventModel, Models.Domain.Event>()
                .ForMember(e => e.Id, conf => conf.MapFrom(y => (y.Id.HasValue ? y.Id.Value : Guid.Empty)))
                .ForMember(e => e.Disabled, conf => conf.Ignore())
                .ForMember(e => e.Places, conf => conf.MapFrom(ee => ee.Places))
                .ForMember(e => e.TimeSlots, conf => conf.Ignore()); 

            Mapper.CreateMap<Models.Domain.Event, Models.Models.Vote.EventViewModel>()
                .ForMember(e => e.Places, conf => conf.MapFrom(ee => ee.Places))
                .ForMember(e => e.TimeSlots, conf => conf.MapFrom(ee => ee.TimeSlots));
        }

        private static void CreatePlaceMap()
        {
            Mapper.CreateMap<Models.Domain.Place, PlaceEntity>()
                .ForMember(pe => pe.VotesForPlace, conf => conf.MapFrom(p => p.VotesForPlace));

            Mapper.CreateMap<PlaceEntity, Models.Domain.Place>()
                .ForMember(p => p.VotesForPlace, conf => conf.MapFrom(pe => pe.VotesForPlace));

            Mapper.CreateMap<Models.Domain.Place, Models.Models.FourSquareVenueModel>()
                .ForMember(p => p.VenueId, conf => conf.MapFrom(p => p.VenueId))
                .ForMember(p => p.Name, conf => conf.Ignore())
                .ForMember(p => p.AddressInfo, conf => conf.Ignore())
                .ForMember(p => p.City, conf => conf.Ignore())
                .ForMember(p => p.Lat, conf => conf.Ignore())
                .ForMember(p => p.Lng, conf => conf.Ignore());


            Mapper.CreateMap<Models.Models.FourSquareVenueModel, Models.Domain.Place>()
                .ForMember(p => p.VenueId, conf => conf.MapFrom(p => p.VenueId))
                .ForMember(p => p.Id, conf => conf.Ignore())
                .ForMember(p => p.EventId, conf => conf.Ignore())
                .ForMember(p => p.VotesForPlace, conf => conf.Ignore());

            Mapper.CreateMap<Models.Domain.Place, Models.Models.Vote.PlaceViewModel>()
                .ForMember(p => p.VotesForPlace, conf => conf.MapFrom(p => p.VotesForPlace));

        }

        private static void CreateTimeSlotMap()
        {
            Mapper.CreateMap<TimeSlotEntity, Models.Domain.TimeSlot>()
                .ForMember(t => t.VotesForDate, conf => conf.ResolveUsing(te => te.VotesForDate));


            Mapper.CreateMap<Models.Domain.TimeSlot, TimeSlotEntity>()
                .ForMember(te => te.VotesForDate, conf => conf.MapFrom(t => t.VotesForDate));

            Mapper.CreateMap<Models.Domain.TimeSlot, Models.Models.Vote.TimeSlotViewModel>()
                .ForMember(t => t.VotesForDate, conf => conf.MapFrom(tt => tt.VotesForDate));
        }

        private static void CreateVoteForDateMap()
        {
            Mapper.CreateMap<VoteForDateEntity, Models.Domain.VoteForDate>()
                .ForMember(v => v.WillAttend, conf => conf.MapFrom(ve => ve.WillAttend));

            Mapper.CreateMap<Models.Domain.VoteForDate, VoteForDateEntity>()
                .ForMember(ve => ve.WillAttend, conf => conf.MapFrom(v => v.WillAttend));
        }

        private static void CreateVoteForPlaceMap()
        {
            Mapper.CreateMap<VoteForPlaceEntity, Models.Domain.VoteForPlace>()
                .ForMember(v => v.WillAttend, conf => conf.MapFrom(ve => ve.WillAttend));

            Mapper.CreateMap<Models.Domain.VoteForPlace, VoteForPlaceEntity>()
                .ForMember(ve => ve.WillAttend, conf => conf.MapFrom(v => v.WillAttend));
            ;
        }

        private static void CreateEnumsMap()
        {
            Mapper.CreateMap<Entities.Enums.WillAttend, Models.Enums.WillAttend>();

            Mapper.CreateMap<Models.Enums.WillAttend, Entities.Enums.WillAttend>();
        }

        private static void CreateUserMap()
        {
            Mapper.CreateMap<Entities.UserEntity, Models.Domain.User>();

            Mapper.CreateMap<Models.Domain.User, Entities.UserEntity>();
        }
    }
}
