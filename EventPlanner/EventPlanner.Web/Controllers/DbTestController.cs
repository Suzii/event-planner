using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models.CreateAndEdit;
using EventPlanner.Models.Models.Shared;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace EventPlanner.Web.Controllers
{
    // solely for testing purposes of DB settings
    [Authorize]
    public class DbTestController : Controller
    {
        public async Task<JsonResult> Index()
        {
            var dates = new List<TimeSlot>();
            dates.Add(new TimeSlot() { DateTime = DateTime.Now });

            // NOTE : Dates will not be stored as there is still an issue with mapping
            var eventModel = new EventModel()
            {
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to drink some beer! Cheers!",
                OthersCanEdit = true,
                ExpectedLength = 2,
                Places = new List<FourSquareVenueModel>() { new FourSquareVenueModel() { VenueId = "1" }, new FourSquareVenueModel() { VenueId = "2 - to be deleted" }, new FourSquareVenueModel() { VenueId = "3" } },
                Dates = new List<EventModel.DatesModel>() { new EventModel.DatesModel() { Date = DateTime.Today, Times = new List<EventModel.TimeModel>() { new EventModel.TimeModel() {Time = "10:00:00"}, new EventModel.TimeModel() {Time = "12:00:00"} } } }
           //     Dates = new List<EventModel.DatesModel>() { new EventModel.DatesModel() {Date = DateTime.Today, Times = new List<string>() {"12:00", "13:30"} } }
            };

            // 1. Store event for the first time
            EventRepository e = new EventRepository();
            var ev = Mapper.Map<Event>(eventModel);
            ev.OrganizerId = User.Identity.GetUserId();
            ev.CreatedOn = DateTime.Today;
            ev = await e.AddOrUpdate(ev);

            // 2. get event from db
            e = new EventRepository();
            var evFromDb = await e.GetFullEvent(ev.Id);
            eventModel = Mapper.Map<Event, EventModel>(evFromDb);

            // 3. update related entities and store again
            eventModel.Places.RemoveAt(2);
            eventModel.Places.Add(new FourSquareVenueModel() { VenueId = "4 - newly added" });
            eventModel.Desc = "Should contain places 1, 3, 4 - newly added";
            var evEdited = Mapper.Map<Event>(eventModel);
            evEdited.OrganizerId = ev.OrganizerId;
            evEdited.CreatedOn = ev.CreatedOn;
            e = new EventRepository();
            evFromDb = await e.AddOrUpdate(evEdited);

            // 4. load updated event
            evFromDb = await e.GetFullEvent(evFromDb.Id);
            eventModel = Mapper.Map<Event, EventModel>(evFromDb);

            // print out
            return Json(eventModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Date()
        {
            return View();
        }

    }

}
