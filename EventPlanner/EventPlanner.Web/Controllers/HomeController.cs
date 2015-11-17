using AutoMapper;
using EventPlanner.Models.Models.Home;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public async Task<ActionResult> Index()
        {
            return View("Index", await ConstructModel(User.Identity.GetUserId()));
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private async Task<MyEventsViewModel> ConstructModel(string userId)
        {
            var result = await _userService.GetEventsCreatedBy(userId);
            var events = result.Select(e => Mapper.Map<EventViewModel>(e)).ToList();
            return new MyEventsViewModel {Events = events};
        }
    }
}