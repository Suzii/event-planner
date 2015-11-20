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
        private readonly IVotingService _votingService;

        private readonly IEventManagementService _eventManagementService;

        private readonly IPlaceService _placeService;

        public VoteAjaxController()
        {
            //TODO: change once real service is up and running
            _votingService = new Services.FakedImplementation.VotingService();
            //TODO: change once real service is up and running
            _eventManagementService = new Services.FakedImplementation.EventManagementService();
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
        public async Task<JsonResult> GetVoteForDateModel(Guid eventid)
        {
            var ev = await _eventManagementService.GetFullEventAsync(eventid);
            var totalNumberOfVoters = await _votingService.GetTotalNumberOfVotersForEvent(eventid);
            var timeSlotsVm = ev.TimeSlots
                .OrderBy(ts => ts.DateTime)
                .Select((ts) => new
                {
                    Title = ts.DateTime.ToShortDateString(),
                    Desc = ts.DateTime.ToLongTimeString(),
                    Id = ts.Id.ToString(),
                    //WillAttend = Model.VotesForDates.Single(v => v.Id == ts.VotesForDate.Where(tv => tv.UserId)).WillAttend.ToString(),
                    //VoteId = Guid.Empty,
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
        public async Task<JsonResult> SubmitVoteForDate(Guid eventId, Guid timeSlotId, Guid? voteForDateId, WillAttend? willAttend)
        {
            var userId = User.Identity.GetUserId();
            // TODO : fix nullable issue
            var voteModel = new VoteForDate()
            {
                Id = voteForDateId.GetValueOrDefault(),
                TimeSlotId = timeSlotId,
                UserId = userId,
                WillAttend = willAttend.GetValueOrDefault()
            };
            await _votingService.SubmitVoteForDate(voteModel);

            var votes = await _votingService.GetVotesForDateAsync(eventId, timeSlotId);
            var data = await ConstructDateVoteResponseModel(eventId, timeSlotId);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitVoteForPlace(Guid eventId, Guid placeId, Guid? voteForDateId, WillAttend? willAttend)
        {
            var userId = User.Identity.GetUserId();
            // TODO : fix nullable issue
            var voteModel = new VoteForPlace()
            {
                Id = voteForDateId.GetValueOrDefault(),
                PlaceId = placeId,
                UserId = userId,
                WillAttend = willAttend.GetValueOrDefault()
            };
            await _votingService.SubmitVoteForPlace(voteModel);

            // TODO : probably create model class for this
            var data = ConstructPlaceVoteResponseModel(eventId, placeId);
            return Json(data);
        }

        private async Task<Object> ConstructDateVoteResponseModel(Guid eventId, Guid timeSlotId)
        {
            var votes = await _votingService.GetVotesForDateAsync(eventId, timeSlotId);
            // TODO : probably create model class for this
            var data = new
            {
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
            var votes = await _votingService.GetVotesForPlaceAsync(eventId, placeId);
            // TODO : probably create model class for this
            var data = new
            {
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

        private List<VoteForPlace> GetUsersPlaceVotes(IList<PlaceViewModel> places, string currentUserId)
        {
            var usersPlaceVotes = places
                .SelectMany(pl => pl.VotesForPlace)
                .Where(vote => vote.UserId == currentUserId)
                .Select(vote => vote);

            var missingPlaceVotes = places
                .Where(pl => !(usersPlaceVotes.Select(pv => pv.PlaceId).Contains(pl.Id)))
                .Select(pl => new VoteForPlace() {PlaceId = pl.Id});

            return usersPlaceVotes.Union(missingPlaceVotes).ToList();
        }

        private List<VoteForDate> GetUsersDateVotes(IList<TimeSlotViewModel> dates, string currentUserId)
        {
            var usersDateVotes = dates
                .SelectMany(ts => ts.VotesForDate)
                .Where(vote => vote.UserId == currentUserId)
                .Select(vote => vote);

            var missingDateVotes = dates
                .Where(ts => !(usersDateVotes.Select(dv => dv.TimeSlotId).Contains(ts.Id)))
                .Select(ds => new VoteForDate() { TimeSlotId = ds.Id });

            return usersDateVotes.Union(missingDateVotes).ToList();
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