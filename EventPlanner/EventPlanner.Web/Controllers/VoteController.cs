using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.DAL.Repository;
using EventPlanner.Entities;
using EventPlanner.Models.Domain;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        public ActionResult Index(string eventHash)
        {
            //get event
            
            return View("Index", ConstructModel(eventHash));
        }

        [Authorize]
        public async Task<Event> Test()
        {
            
            EventRepository e = new EventRepository();
            return await e.AddOrUpdate(new Event()
            {
                Id = Guid.NewGuid(),
                Title = "Some fake event for testing purposes",
                Desc = "Hello there, we are going to dring some beer! Cheers!",
                Created = DateTime.Now,
                OthersCanEdit = true,
                ExpectedLength = 2,
                OrganizerId = User.Identity.GetUserId(),
                Places = new List<Place>(),
                TimeSlots = new List<TimeSlot>()
            });
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
                OrganizerId = Guid.NewGuid().ToString(),
                Places = new List<Place>(),
                TimeSlots = new List<TimeSlot>()
            };
        }
    }
}