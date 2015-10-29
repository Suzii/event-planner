using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPlanner.Models.Domain;

namespace EventPlanner.Web.Controllers
{
    public class VoteController : Controller
    {
        public ActionResult Index(string eventHash)
        {
            //get event
            
            return View("Index", ConstructModel(eventHash));
        }

        private Event ConstructModel(string eventHash)
        {
            // obtain the Event object from service based on its hash code

            // fake implementation
            return new Event()
            {
                Id = Guid.NewGuid(),
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to dring some beer! Cheers!",
                Created = DateTime.Now,
                OthersCanEdit = true,
                ExpectedLength = 2,
                OrganizerId = Guid.NewGuid(),
                Places = new List<PlaceEntity>(),
                TimeSlots = new List<TimeSlot>()
            };
        }
    }
}