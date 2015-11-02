using System;
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

        //TODO castle inject
        public VoteController()
        {
            _votingService = new VotingService();
            _eventManagementService = new EventPlanner.Services.FakedImplementation.EventManagementService();
        }

        public async Task<ActionResult> Index(Guid? id)
        {
            var model = await ConstructModel(id ?? Guid.NewGuid());
            return View("Index", model);
        }

        private async Task<EventViewModel> ConstructModel(Guid id)
        {
            // obtain the Event object from service based on its hash code
            var result = await _eventManagementService.GetEvent(id);

            var eventViewModel =  Mapper.Map<EventViewModel>(result);
            return eventViewModel;

            //// fake implementation
            //return new EventViewModel()
            //{
            //    Id = Guid.NewGuid(),
            //    Title = "Some fake event for testing purposes",
            //    Desc = "Hello there, we are going to dring some beer! Cheers!",
            //    Created = DateTime.Now,
            //    OthersCanEdit = true,
            //    ExpectedLength = 2,
            //    OrganizerId = Guid.NewGuid(),
            //    Places = new List<PlaceViewModel>(),
            //    TimeSlots = new List<TimeSlotViewModel>()
            //};
        }
    }
}