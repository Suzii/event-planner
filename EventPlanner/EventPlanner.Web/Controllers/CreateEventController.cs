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
    }
}