using System.Web.Mvc;
using EventPlanner.Models;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;

namespace EventPlanner.Web.Controllers
{
    public class EventOrganizationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", ConstructModel());
        }

        [HttpPost]
        public ActionResult Index(Event model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //create event in database
            //obtain hash code
            var eventId = model.Hash;
            
            return RedirectToAction("ShareEvent", new {id = eventId});
        }

        public ActionResult ShareEvent(string id)
        {
            var url = Url.Action("Index", "Vote", new { id = id }, Request.Url.Scheme);
            return View("ShareEvent", new ShareLinkModel(){ Link = url, EventHash = id});
        }

        private Event ConstructModel()
        {
            return new Event();
        }

    }

}