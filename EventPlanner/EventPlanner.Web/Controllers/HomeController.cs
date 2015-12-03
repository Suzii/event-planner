using AutoMapper;
using EventPlanner.Models.Models.Home;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Services;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public async Task<ActionResult> Index()
        {
            return View("Index", await ConstructModel(User.Identity.GetUserId()));
        }

        private async Task<MyEventsViewModel> ConstructModel(string userId)
        {
            var result = await _userService.GetEventsCreatedBy(userId);
            var events = result.Select(Mapper.Map<EventInfoViewModel>).ToList();
            return new MyEventsViewModel {Events = events};
        }
    }
}