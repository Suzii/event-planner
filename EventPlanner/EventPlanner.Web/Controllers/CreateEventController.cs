using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models;
using EventPlanner.Services;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class CreateEventController : Controller
    {
        private readonly IEventManagementService _eventManagementService;

        public CreateEventController()
        {
            _eventManagementService = new EventManagementService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eventHash)
        {
            var model = (eventHash == null) ? ConstructModel() : await GetModel(eventHash);
            return View("Index", ConstructModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(EventModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var ev = Mapper.Map<Event>(model);
            ev.OrganizerId = User.Identity.GetUserId();
            var eventEntity = await _eventManagementService.CreateEventAsync(ev);
            
            var eventHash = eventEntity.Hash;
            return RedirectToAction("Index", "ShareEvent", new {eventHash = eventHash });
        }

        private EventModel ConstructModel()
        {
            return new EventModel();
        }

        private async Task<EventModel> GetModel(string eventHash)
        {
            var eventId = _eventManagementService.GetEventId(eventHash);
            var result = await _eventManagementService.GetEventAsync(eventId);

            return Mapper.Map<EventModel>(result);
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