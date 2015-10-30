using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventPlanner.Models.Models;
using EventPlanner.Services.Implementation;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
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
        public async Task<JsonResult> GetData(string city, string query)
        {
            if (city == null || query == null)
            {
                throw new ArgumentException("FoursquareRequest");
            }

            var ps = new PlaceService();

            var response = await ps.GetPlaceSuggestionsAsync(query, city);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}