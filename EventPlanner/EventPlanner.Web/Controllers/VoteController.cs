using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;
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

        //TODO castle inject
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
            // obtain the Event object from service based on its hash code
            var result = await _eventManagementService.GetEventAsync(id);

            var eventViewModel =  Mapper.Map<EventViewModel>(result);
            var venuesDetails = await _placeService.GetPlacesDetailsAsync(eventViewModel.Places.Select(p => p.VenueId).ToList());
            foreach (var place in eventViewModel.Places)
            {
                place.Venue = venuesDetails.Single(p => p.VenueId == place.VenueId);
            }

            var allUsers = eventViewModel.Places.SelectMany(p => p.VotesForPlaceBy.Select(v => v.UserId).ToList()).Distinct();
            

            return eventViewModel;
        }

        public async Task<Event> Test()
        {
            var dates = new List<TimeSlot>();
            dates.Add(new TimeSlot() { DateTime = DateTime.Now });

            EventRepository e = new EventRepository();
            var eventModel = new EventModel()
            {
                Id = Guid.NewGuid(),
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to drink some beer! Cheers!",
                Created = DateTime.Now,
                OthersCanEdit = true,
                ExpectedLength = 2,
                OrganizerId = Guid.NewGuid(),
                Places = new List<FourSquareVenueModel>(),
                TimeSlots = dates
            };

            var ev = Mapper.Map<Event>(eventModel);
            ev.OrganizerId = User.Identity.GetUserId();

            return await e.AddOrUpdate(ev);
        }
    }
}