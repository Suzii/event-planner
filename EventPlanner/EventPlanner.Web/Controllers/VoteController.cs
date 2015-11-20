using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Enums;
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
            //TODO: change once real service is up and running
            _eventManagementService = new Services.FakedImplementation.EventManagementService();
            _placeService = new PlaceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eventHash)
        {
            var id = _eventManagementService.GetEventId(eventHash);
            var eventViewmodel = await ConstructEventViewModel(id);
            
            // TODO: use this with real EventManagementService: User.Identity.GetUserId();
            var currentUserId = "Suzi"; 
            var model = ConstructModel(eventViewmodel, currentUserId);
            return View("Index", model);
        }

        //TODO : model binding refactoring needed
        public async Task<ActionResult> Index(IList<VoteForPlace> VotesForPlaces, IList<VoteForDate> VotesForDates)
        {
            if (!ModelState.IsValid)
            {
                //TODO: will have to work via ajax..
                //return View("Index", new VoteModel() {VotesForPlaces = VotesForPlaces, VotesForDates = VotesForDates});
            }
            var userId = User.Identity.GetUserId();
            await _votingService.SubmitPlaceVotesByAsync(userId, VotesForPlaces);
            await _votingService.SubmitDateVotesByAsync(userId, VotesForDates);
            
            return RedirectToAction("Index");
        }

        private object ConstructModel(EventViewModel eventViewmodel, string currentUserId)
        {
            var model = new VoteModel();
            model.EventViewModel = eventViewmodel;
            eventViewmodel.Places = eventViewmodel.Places.OrderBy(pl => pl.VotesForPlace.Count(v => v.WillAttend == WillAttend.Yes)).ToList();
            eventViewmodel.TimeSlots = eventViewmodel.TimeSlots.OrderBy(ts => ts.DateTime).ToList();
            model.VotesForPlaces = GetUsersPlaceVotes(eventViewmodel.Places, currentUserId);
            model.VotesForDates = GetUsersDateVotes(eventViewmodel.TimeSlots, currentUserId);

            return model;
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

        private async Task<EventViewModel> ConstructEventViewModel(Guid id)
        {
            var result = await _eventManagementService.GetFullEventAsync(id);

            var eventViewModel =  Mapper.Map<EventViewModel>(result);
            await PopulateVenueDetails(eventViewModel);


            var allUsers = eventViewModel.Places.SelectMany(p => p.VotesForPlace.Select(v => v.UserId).ToList()).Distinct();
            eventViewModel.TotalNumberOfVoters = allUsers.Count();
            
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