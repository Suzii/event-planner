using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;
using EventPlanner.Models.Models.CreateAndEdit;
using EventPlanner.Models.Models.Shared;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    // solely for testing purposes of DB settings
    public class DbTestController : Controller
    {
        public async Task<JsonResult> Test()
        {
            var dates = new List<TimeSlot>();
            dates.Add(new TimeSlot() { DateTime = DateTime.Now });

            EventRepository e = new EventRepository();
            var eventModel = new EventModel()
            {
                //Id = Guid.NewGuid(),
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to drink some beer! Cheers!",
                OthersCanEdit = true,
                ExpectedLength = 2,
                Places = new List<FourSquareVenueModel>() { new FourSquareVenueModel() { VenueId = "529ebe0f498eee32aa9dee7e" }} ,
                Dates = new List<EventModel.DatesModel>() { new EventModel.DatesModel() {Date = DateTime.Today, Times = new List<string>() {"12:00", "13:30"} } }
            };

            var ev = Mapper.Map<Event>(eventModel);
            ev.OrganizerId = User.Identity.GetUserId();
            ev.CreatedOn = DateTime.Today;

            ev = await e.AddOrUpdate(ev);

            EventRepository er = new EventRepository();
            var evFromDb = await er.GetFullEvent(ev.Id);

            return Json(evFromDb, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Date()
        {
            return View();
        }

    }

}
