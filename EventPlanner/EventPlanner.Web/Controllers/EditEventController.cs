using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPlanner.Models.Domain;

namespace EventPlanner.Web.Controllers
{
    public class EditEventController : Controller
    {
        [HttpGet]
        public ActionResult Index(string eventHash)
        {
            return View("Index", ConstructModel(eventHash));
        }

        [HttpPost]
        public ActionResult Index(EventEntity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // store event in database via service
            // check server validation

            // obtain id + date created and calculate hash code
            var eventHash = model.Hash;

            return RedirectToAction("Index", "ShareEvent", new { eventHash = eventHash });
        }

        private EventEntity ConstructModel(string eventHash)
        {
            // obtain the Event object from service based on its hash code

            // fake implementation
            return new EventEntity()
            {
                Id = Guid.NewGuid(),
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to dring some beer! Cheers!",
                Created = DateTime.Now,
                OthersCanEdit = true,
                ExpectedLength = 2,
                OrganizerId = Guid.NewGuid(),
                Places = new List<PlaceEntity>(),
                TimeSlots = new List<TimeSlotEntity>()
            };
        }
    }
}