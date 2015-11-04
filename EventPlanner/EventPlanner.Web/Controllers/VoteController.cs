using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.Models.Models.Vote;
using EventPlanner.Services;
using EventPlanner.Services.Implementation;

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
            var model = await ConstructModel(id);
            return View("Index", model);
        }

        private async Task<EventViewModel> ConstructModel(Guid id)
        {
            var result = await _eventManagementService.GetEventAsync(id);

            var eventViewModel =  Mapper.Map<EventViewModel>(result);
            //FAKES
            eventViewModel.Places = new List<PlaceViewModel>()
            {
                new PlaceViewModel() {VenueId = "529ebe0f498eee32aa9dee7e"},
                new PlaceViewModel() {VenueId = "51470131e4b0ff6e39c2fb73"}

            };

            var venuesDetails = await _placeService.GetPlacesDetailsAsync(eventViewModel.Places.Select(p => p.VenueId).ToList());
            foreach (var place in eventViewModel.Places)
            {
                place.Venue = venuesDetails.Single(p => p.VenueId == place.VenueId);
                //FAKES
                place.VotesForPlaceBy = new List<VoteForPlaceByViewModel>()
                {
                    new VoteForPlaceByViewModel() {WillAttend = true, UserName = "Tomas"},
                    new VoteForPlaceByViewModel() {WillAttend = false, UserName = "Janka"},
                    new VoteForPlaceByViewModel() {WillAttend = false, UserName = "Jirka"},
                    new VoteForPlaceByViewModel() {WillAttend = true, UserName = "Martin"}
                };
            }

            var allUsers = eventViewModel.Places.SelectMany(p => p.VotesForPlaceBy.Select(v => v.UserId).ToList()).Distinct();
            
            return eventViewModel;
        }
    }
}