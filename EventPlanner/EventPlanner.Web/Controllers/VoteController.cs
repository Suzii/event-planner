using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.AutoMappers;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Enums;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Models.Models.Vote;
using EventPlanner.Services;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private readonly IVotingService _votingService;

        private readonly IEventManagementService _eventManagementService;

        private readonly IPlaceService _placeService;

        public VoteController()
        {
            _votingService = new VotingService();
            _eventManagementService = new EventManagementService();
            _placeService = new PlaceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eventHash)
        {
            var id = _eventManagementService.GetEventId(eventHash);
            var eventViewmodel = await ConstructEventViewModel(id);
            return View("Index", eventViewmodel);
        }

        [HttpGet]
        public async Task<JsonResult> GetVoteForDateModel(Guid eventId)
        {
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);
            var timeSlots = await _votingService.GetDatesWithVotes(eventId);
            var optionsVm = timeSlots
                .OrderBy(ts => ts.DateTime)
                .Select((ts) => MappingHelper.MapToOptionViewModel(ts, User.Identity.GetUserId()))
                .ToList();

            return Json(new {Options = optionsVm, TotalNumberOfVoters = totalNumberOfVoters},
                JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public async Task<JsonResult> GetVoteForPlaceModel(Guid eventId)
        {
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);
            var places = await _votingService.GetPlacesWithVotes(eventId);
            var placesVm = places.Select(Mapper.Map<PlaceViewModel>).ToList();
            await _placeService.PopulateVenueDetailsAsync(placesVm);
            var optionsVm = placesVm
                .OrderBy(pl => pl.VotesForPlace.Count(v => v.WillAttend == WillAttend.Yes))
                .Select((pl) => MappingHelper.MapToOptionViewModel(pl, User.Identity.GetUserId()))
                .ToList();

            return Json(new { Options = optionsVm, TotalNumberOfVoters = totalNumberOfVoters },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForDate(Guid eventId, Guid optionId, Guid usersVoteId, WillAttend? willAttend)
        {
            var userId = User.Identity.GetUserId();
            var voteModel = new VoteForDate()
            {
                Id = usersVoteId,
                TimeSlotId = optionId,
                UserId = userId,
                WillAttend = willAttend
            };

            await _votingService.SubmitVoteForDate(voteModel);

            var votes = await _votingService.GetVotesForDateAsync(eventId, optionId);
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);

            return Json(new
            {
                Option = new OptionViewModel()
                {
                    UsersVote = MappingHelper.MapToUsersVoteModel(votes, userId),
                    Votes = MappingHelper.MapToVotesViewModel(votes)
                },
                TotalNumberOfVoters = totalNumberOfVoters
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForPlace(Guid eventId, Guid optionId, Guid usersVoteId, WillAttend? willAttend)
        {
            var userId = User.Identity.GetUserId();
            var voteModel = new VoteForPlace()
            {
                Id = usersVoteId,
                PlaceId = optionId,
                UserId = userId,
                WillAttend = willAttend
            };

            await _votingService.SubmitVoteForPlace(voteModel);

            var votes = await _votingService.GetVotesForPlaceAsync(eventId, optionId);
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);

            return Json(new
            {
                Option = new OptionViewModel()
                {
                    UsersVote = MappingHelper.MapToUsersVoteModel(votes, userId),
                    Votes = MappingHelper.MapToVotesViewModel(votes)
                },
                TotalNumberOfVoters = totalNumberOfVoters
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetPlacesModelForMap(Guid eventId)
        {
            throw new NotImplementedException();
        }

        private async Task<EventInfoViewModel> ConstructEventViewModel(Guid id)
        {
            var result = await _eventManagementService.GetEventInfoAsync(id);
            var eventViewModel =  Mapper.Map<EventInfoViewModel>(result);
            return eventViewModel;
        }
    }
}