using System.Web.Mvc;

namespace EventPlanner.Web.Controllers
{
    public class EditEventController : Controller
    {
        [HttpGet]
        public ActionResult Index(string eventHash)
        {
            //TODO - decide whether this controller is really necessary 
            return RedirectToAction("Index", "CreateEvent", new { eventHash = eventHash });
        }
    }
}