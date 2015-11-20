using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models.CreateAndEdit;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Services;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    // solely for testing purposes of DB settings
    [Authorize]
    public class DbTestController : Controller
    {
        private readonly IEventManagementService _eventManagementService;

        public DbTestController()
        {
            _eventManagementService = new Services.FakedImplementation.EventManagementService();
        }

        public async Task<JsonResult> Index()
        {
            var ems = new EventManagementService();

            // NOTE : Dates will not be stored as there is still an issue with mapping
            var eventModel = new EventModel()
            {
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to drink some beer! Cheers!",
                OthersCanEdit = true,
                ExpectedLength = 2,
                Places = new List<FourSquareVenueModel>() { new FourSquareVenueModel() { VenueId = "1" }, new FourSquareVenueModel() { VenueId = "2 - to be deleted" }, new FourSquareVenueModel() { VenueId = "3" } },
                Dates = new List<EventModel.DatesModel>() { new EventModel.DatesModel() { Date = DateTime.Today, Times = new List<EventModel.TimeModel>() { new EventModel.TimeModel() {Time = "10:00:00"}, new EventModel.TimeModel() {Time = "12:00:00"} } } }
            };

            var e = Mapper.Map<Event>(eventModel);
            // 1. Store event for the first time
            e = await ems.CreateEventAsync(e, User.Identity.GetUserId());

            eventModel = Mapper.Map<EventModel>(e);

            // 2. get event from db
            //e = new EventRepository();
            //var evFromDb = await e.GetFullEvent(ev.Id);
            //eventModel = Mapper.Map<Event, EventModel>(evFromDb);

            // 3. update related entities and store again
            eventModel.Places.RemoveAt(1); // do we have to handle this manually in service?
            eventModel.Places[0].VenueId += "(edited)";
            eventModel.Places.Add(new FourSquareVenueModel() { VenueId = "4 - newly added"/*, EventId = eventModel.Id.Value*/});
            eventModel.Desc = "Should contain places 1(edited), 3, 4 - newly added";
            //var evEdited = Mapper.Map<Event>(eventModel);
            //evEdited.OrganizerId = ev.OrganizerId;
            //evEdited.CreatedOn = ev.CreatedOn;
            //e = new EventRepository();
            //evFromDb = await e.AddOrUpdate(evEdited);
            e = Mapper.Map<Event>(eventModel);
            e = await ems.UpdateEventAsync(e);
            eventModel = Mapper.Map<EventModel>(e);

            // 4. load updated event
            //evFromDb = await e.GetFullEvent(evFromDb.Id);
            //eventModel = Mapper.Map<Event, EventModel>(evFromDb);

            // print out
            return Json(eventModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call this method before you want to use voting AJAX page.
        /// Save the ID of newly created faked event.
        /// Copy this is and assign it to the EVENT_ID constant in VoteAjaxController
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> CreateFakeEventWIthVotes()
        {
            var ev = await _eventManagementService.CreateEventAsync(null, User.Identity.GetUserId());
            return Json(ev, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Date()
        {
            return View();
        }

    }

}
