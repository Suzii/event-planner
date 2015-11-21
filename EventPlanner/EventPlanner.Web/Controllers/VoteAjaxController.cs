using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
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
        /// <summary>
        /// !!!!!!! This is for development purposes only !!!!!!. 
        /// When starting with blank database:
        /// 1. Open /DbTest/CreateFakeEventWIthVotes
        /// 2. Save id of newly saved event
        /// 3. Replace FAKED_EVENT_ID with newly generated value
        /// 4. You can use voting AJAX page which will work with faked data in DB.
        /// </summary>
        private readonly Guid FAKED_EVENT_ID = new Guid("61CC75CE-CB8F-E511-9BD1-685D43C38268");

        private readonly IVotingService _votingService;

        private readonly IEventManagementService _eventManagementService;

        private readonly IPlaceService _placeService;

        public VoteAjaxController()
        {
            //TODO: change once real service is up and running
            _votingService = new VotingService();
            //TODO: change once real service is up and running
            _eventManagementService = new Services.Implementation.EventManagementService();
            _placeService = new PlaceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eventHash)
        {
            var id = _eventManagementService.GetEventId(eventHash);

#warning testing only - will ensure all other methods will be called with this value as it is injected to JS component that does other requests
            id = FAKED_EVENT_ID;
            var eventViewmodel = await ConstructEventViewModel(id);
            return View("Index", eventViewmodel);
        }

        [HttpGet]
        public async Task<JsonResult> GetVoteForDateModel(Guid eventId)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventId);
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId);
            var timeSlotsVm = ev.TimeSlots
                .OrderBy(ts => ts.DateTime)
                .Select((ts) => new
                {
                    Title = ts.DateTime.ToShortDateString(),
                    Desc = ts.DateTime.ToLongTimeString(),
                    Id = ts.Id.ToString(),
                    UsersVote = new
                    {
                        Id = ts.VotesForDate.SingleOrDefault(v => v.UserId == User.Identity.GetUserId())?.Id ?? Guid.Empty,
                        WillAttend = ts.VotesForDate.SingleOrDefault(v => v.UserId == User.Identity.GetUserId())?.WillAttend ?? null
                    },
                    
                    Votes = new
                    {
                        Yes = ts.VotesForDate?.Where(vote => vote.WillAttend == WillAttend.Yes).Select(vote => vote.UserId).ToArray() ?? new string[] { },
                        Maybe = ts.VotesForDate?.Where(vote => vote.WillAttend == WillAttend.Maybe).Select(vote => vote.UserId).ToArray() ?? new string[] { },
                        No = ts.VotesForDate?.Where(vote => vote.WillAttend == WillAttend.No).Select(vote => vote.UserId).ToArray() ?? new string[] { },
                    }
                }).ToList();

            return Json(new { timeSlots = timeSlotsVm, totalNumberOfVoters = totalNumberOfVoters }, JsonRequestBehavior.AllowGet);
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
                TimeSlotId = timeSlotId,
                UserId = userId,
                WillAttend = willAttend
            };
            voteModel = await _votingService.SubmitVoteForDate(voteModel);

            var data = await ConstructDateVoteResponseModel(eventId, timeSlotId, voteModel.Id);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForPlace(Guid eventId, Guid placeId, Guid? voteForDateId, WillAttend? willAttend)
        {
            throw new NotImplementedException();
        }

        private async Task<Object> ConstructDateVoteResponseModel(Guid eventId, Guid timeSlotId, Guid voteId)
        {
            var votes = await _votingService.GetVotesForDateAsync(eventId, timeSlotId);
            // TODO : probably create model class for this
            var data = new
            {
                usersVote = new
                {
                    id = voteId
                },
                totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventId),
                votes = new
                {
                    // TODO : change userId to userName once supported
                    yes = votes.Where(vote => vote.WillAttend == WillAttend.Yes).Select(vote => vote.UserId),
                    maybe = votes.Where(vote => vote.WillAttend == WillAttend.Maybe).Select(vote => vote.UserId),
                    no = votes.Where(vote => vote.WillAttend == WillAttend.No).Select(vote => vote.UserId)
                }
            };
            return data;
        }

        private async Task<Object> ConstructPlaceVoteResponseModel(Guid eventId, Guid placeId)
        {
            throw new NotImplementedException();
        }

        private async Task<EventInfoViewModel> ConstructEventViewModel(Guid id)
        {
            var result = await _eventManagementService.GetEventInfoAsync(id);
            var eventViewModel =  Mapper.Map<EventInfoViewModel>(result);
            return eventViewModel;
        }
        
        private async Task PopulateVenueDetails(EventViewModel eventViewModel)
        {
            var venuesDetails = await _placeService.GetPlacesDetailsAsync(eventViewModel.Places.Select(p => p.VenueId).ToList());
            foreach (var place in eventViewModel.Places)
            {
                place.Venue = venuesDetails.Single(p => p.VenueId == place.VenueId);
            }
        }
    }
}