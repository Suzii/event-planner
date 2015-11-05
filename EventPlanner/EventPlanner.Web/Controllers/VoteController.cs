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
            //TODO: change once real service is up and running
            _eventManagementService = new Services.FakedImplementation.EventManagementService();
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