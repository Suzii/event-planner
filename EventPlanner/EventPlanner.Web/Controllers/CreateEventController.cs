using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EventPlanner.Models.Domain;

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
            
            return RedirectToAction("Index", "ShareEvent", new {eventHash = eventHash });
        }

        private EventEntity ConstructModel()
        {
            return new EventEntity();
        }

        [HttpPost]
        public JsonResult GetData(string city, string query)
        {
            if (city == null || query == null)
            {
                throw new ArgumentException("FoursquareRequest");
            }

            var response = new List<FoursquareVenue>()
            {
                new FoursquareVenue() { name = "Some sushi", id = 12},
                new FoursquareVenue() { name = "Play football", id = 42},
                new FoursquareVenue() { name = "Go bowling", id = 65},
                new FoursquareVenue() { name = "Best pizza in Prague", id = 23},
                new FoursquareVenue() { name = "Basketball", id = 56},
                new FoursquareVenue() { name = "Lovely park", id = 154645},
                new FoursquareVenue() { name = "Cinema 3D", id = 3242},
                new FoursquareVenue() { name = "No idea what this place is supposed to be", id = 1342},
            };

            //response = response.FindAll(o => o.name.IndexOf(city, StringComparison.OrdinalIgnoreCase) >= 0);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public class FoursquareRequerst
        {
            public string City { get; set; }
            public string Query { get; set; }
        }

        public class FoursquareVenue
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        public class Event
        {
            public string Name { get; set; }
            public IList<Place> Places { get; set; }
        }

        public class Place
        {
            public int VenueId { get; set; }
            public string Desc { get; set; }
        }
    }
}