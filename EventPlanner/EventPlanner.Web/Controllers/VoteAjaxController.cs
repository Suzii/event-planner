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
    public class VoteAjaxController : Controller
    {
        private readonly IVotingService _votingService;

        private readonly IEventManagementService _eventManagementService;

        private readonly IPlaceService _placeService;

        public VoteAjaxController()
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
        public async Task<JsonResult> GetVoteForPlaceModel(Guid eventid)
        {
            //Places.OrderBy(pl => pl.VotesForPlace.Count(v => v.WillAttend == WillAttend.Yes)).ToList();
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForDate(Guid eventId, Guid timeSlotId, Guid voteForDateId, WillAttend? willAttend)
        {
            var userId = User.Identity.GetUserId();
            var voteModel = new VoteForDate()
            {
                Id = voteForDateId,
                OptionId = timeSlotId,
                UserId = userId,
                WillAttend = willAttend
            };

            await _votingService.SubmitVoteForDate(voteModel);

            var votes = await _votingService.GetVotesForDateAsync(eventId, timeSlotId);
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);

            return Json(new
            {
                Option = new OptionViewModel()
                {
                    UsersVote = MappingHelper.MapUsersVoteModel(votes, userId),
                    Votes = MappingHelper.MapToVotesViewModel(votes)
                },
                TotalNumberOfVoters = totalNumberOfVoters
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForPlace(Guid eventId, Guid placeId, Guid? voteForDateId, WillAttend? willAttend)
        {
            throw new NotImplementedException();
        }

        private async Task<EventInfoViewModel> ConstructEventViewModel(Guid id)
        {
            var result = await _eventManagementService.GetEventInfoAsync(id);
            var eventViewModel =  Mapper.Map<EventInfoViewModel>(result);
            return eventViewModel;
        }
        
        private async Task PopulateVenueDetails(IList<PlaceViewModel> places)
        {
            var venuesDetails = await _placeService.GetPlacesDetailsAsync(places.Select(p => p.VenueId).ToList());
            foreach (var place in places)
            {
                place.Venue = venuesDetails.Single(p => p.VenueId == place.VenueId);
            }
        }
    }
}