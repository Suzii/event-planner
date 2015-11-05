using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPlaceService _placeService;

        public CreateEventController()
        {
            _eventManagementService = new EventManagementService();
            _placeService = new PlaceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eventHash)
        {
            var model = (eventHash == null) ? ConstructModel() : await GetModel(eventHash);
            return View("Index", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(EventModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //update eventEtity
            var eventEntity = Mapper.Map<Event>(model);
            eventEntity.TimeSlots = model.Dates.SelectMany(d => d.Times.Select(time => new TimeSlot()
            {
                DateTime = d.Date.Add(TimeSpan.Parse(time))
            }).ToList()).ToList();

            eventEntity = (model.Id.HasValue) ?
                await _eventManagementService.UpdateEventAsync(eventEntity) : 
                await _eventManagementService.CreateEventAsync(eventEntity, User.Identity.GetUserId());

            return RedirectToAction("Index", "ShareEvent", new {eventHash = eventEntity.Hash });
        }

        private EventModel ConstructModel()
        {
            return new EventModel()
            {
                ExpectedLength = 1,
                Dates = new List<EventModel.DatesModel>()
                {
                    new EventModel.DatesModel()
                    {
                        Date = DateTime.Today,
                        Times = new List<string>()
                        {
                            "00:00"
                        }
                    }
                }
            };
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
                throw new ArgumentException("FoursquareRequest: both city and query have to be entered.");
            }

            var response = await _placeService.GetPlaceSuggestionsAsync(query, city);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}