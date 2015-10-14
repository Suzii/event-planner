using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPlanner.Models.Domain;

namespace EventPlanner.Web.Controllers
{
    public class VoteController : Controller
    {
        public ActionResult Index(string id)
        {
            //get event
            
            return View("Index", ConstructModel());
        }

        private Event ConstructModel()
        {
            return new Event()
            {
                Title = "Some mocked event",
                Desc = "No description provided"
            };
        }
    }
}