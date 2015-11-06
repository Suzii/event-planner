using AutoMapper;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        public async Task<ActionResult> Index()
        {
            var result = await _userService.GetEventsCreatedBy(User.Identity.GetUserId());
            IList<EventModel> model = new List<EventModel>();

            foreach (Event e in result)
            {
                model.Add(Mapper.Map<EventModel>(e));
            }

            return View("Index", new MyEventsViewModel { Events = model });
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
    }
}