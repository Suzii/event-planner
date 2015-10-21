using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;

namespace EventPlanner.Web.Controllers
{
    public class CreateEventController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", ConstructModel());
        }

        [HttpPost]
        public ActionResult Index(EventModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // store event in database via service
            // check server validation
            
            // obtain Id + date created and calculate hash code
            var eventHash = model.Hash;
            
            return RedirectToAction("Index", "ShareEvent", new {eventHash = eventHash });
        }

        private EventModel ConstructModel()
        {
            return new EventModel();
        }

        [HttpGet]
        public JsonResult GetData(string city, string query)
        {
            if (city == null || query == null)
            {
                throw new ArgumentException("FoursquareRequest");
            }

            var response = new List<FourSquareVenueModel>()
            {
                new FourSquareVenueModel() { Name = "Some sushi", VenueId = 12},
                new FourSquareVenueModel() { Name = "Play football", VenueId = 42},
                new FourSquareVenueModel() { Name = "Go bowling", VenueId = 65},
                new FourSquareVenueModel() { Name = "Best pizza in Prague", VenueId = 23},
                new FourSquareVenueModel() { Name = "Basketball", VenueId = 56},
                new FourSquareVenueModel() { Name = "Lovely park", VenueId = 154645},
                new FourSquareVenueModel() { Name = "Cinema 3D", VenueId = 3242},
                new FourSquareVenueModel() { Name = "No Idea what this place is supposed to be", VenueId = 1342},
            };

            response = response.FindAll(o => o.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}