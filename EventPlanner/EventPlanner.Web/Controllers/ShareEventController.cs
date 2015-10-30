using System.Web.Mvc;
using EventPlanner.Models.Models;
using EventPlanner.Models.Models.Share;

namespace EventPlanner.Web.Controllers
{
    public class ShareEventController : Controller
    {
        [HttpGet]
        public ActionResult Index(string eventHash)
        {
            var url = Url.Action("Index", "Vote", new { id = eventHash }, Request.Url.Scheme);

            return View(new ShareLinkViewModel()
            {
                Link = url,
                EventHash = eventHash
            });
        }
    }
}