using System.Web.Mvc;
using EventPlanner.Models.Models.Share;

namespace EventPlanner.Web.Controllers
{
    public class ShareEventController : Controller
    {
        [HttpGet]
        public ActionResult Index(string eventHash)
        {
            return View("Index", ConstructViewModel(eventHash));
        }

        private ShareLinkViewModel ConstructViewModel(string eventHash)
        {
            var url = Url.Action("Index", "Vote", new { eventHash = eventHash }, Request.Url.Scheme);

            return new ShareLinkViewModel()
            {
                Link = url,
                EventHash = eventHash
            };
        }
    }
}