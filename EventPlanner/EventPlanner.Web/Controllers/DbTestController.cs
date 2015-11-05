using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    // solely for testing purposes of DB settings
    public class DbTestController : Controller
    {
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
                Places = new List<FourSquareVenueModel>() {new FourSquareVenueModel() { VenueId = "529ebe0f498eee32aa9dee7e" }} ,
                //TimeSlots = dates
            };

            var ev = Mapper.Map<Event>(eventModel);
            ev.OrganizerId = User.Identity.GetUserId();

            return await e.AddOrUpdate(ev);
        }


        public ActionResult Date()
        {
            return View();
        }

    }

}
