using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models.CreateAndEdit;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Services;
using EventPlanner.Services.Implementation;
using Microsoft.AspNet.Identity;
using EventPlanner.Web.Helpers;

namespace EventPlanner.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventManagementService _eventManagementService;
        private readonly IPlaceService _placeService;

        public EventController()
        {
            _eventManagementService = new EventManagementService();
            _placeService = new PlaceService();
            List<Object> list = new List<Object>
            {
               new {Value = "00:00:00", Text = "00:00"},
               new {Value = "01:00:00", Text = "01:00"}, 
               new {Value = "02:00:00", Text = "02:00"},
               new {Value = "03:00:00", Text = "03:00"},
               new {Value = "04:00:00", Text = "04:00"},
               new {Value = "05:00:00", Text = "05:00"},
               new {Value = "06:00:00", Text = "06:00"},
               new {Value = "07:00:00", Text = "07:00"},
               new {Value = "08:00:00", Text = "08:00"},
               new {Value = "09:00:00", Text = "09:00"},
               new {Value = "10:00:00", Text = "10:00"},
               new {Value = "11:00:00", Text = "11:00"},
               new {Value = "12:00:00", Text = "12:00"},
               new {Value = "13:00:00", Text = "13:00"},
               new {Value = "14:00:00", Text = "14:00"},
               new {Value = "15:00:00", Text = "15:00"},
               new {Value = "16:00:00", Text = "16:00"},
               new {Value = "17:00:00", Text = "17:00"},
               new {Value = "18:00:00", Text = "18:00"},
               new {Value = "19:00:00", Text = "19:00"},
               new {Value = "20:00:00", Text = "20:00"},
               new {Value = "21:00:00", Text = "21:00"},
               new {Value = "22:00:00", Text = "22:00"},
               new {Value = "23:00:00", Text = "23:00"}  
            };
           
            ViewBag.Times = list;
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
            
            return RedirectToAction("Index", "ShareEvent", new {eventHash = eventEntity.Id.GetUniqueUrlParameter() });
        }
       
        private EventModel ConstructModel()
        {
            return new EventModel()
            {
                ExpectedLength = 1,
                Dates = GetDefaultDatesModel()

            };
        }


        private async Task<EventModel> GetModel(string eventHash)
        {
            var eventId = _eventManagementService.GetEventId(eventHash);
            var result = await _eventManagementService.GetFullEventAsync(eventId);

           
            var model = Mapper.Map<EventModel>(result);
            if (model.Places == null)
            {
                model.Places = new List<FourSquareVenueModel>();
            }
            await PopulateVenueDetails(model);

            if (result.TimeSlots == null)
            {
                model.Dates = GetDefaultDatesModel();
            }
            else
            {
                model.Dates =
                    result.TimeSlots.GroupBy(ts => ts.DateTime.Date, ts => ts)
                        .Select(
                            tsGrp =>
                                new EventModel.DatesModel()
                                {
                                    Date = tsGrp.Key.Date,
                                    Times = tsGrp.Select(ts => ts.DateTime.ToLongTimeString()).ToList()
                                }).ToList();

                //foreach (var date in result.TimeSlots.Select(ts => ts.DateTime).Distinct())
                //{
                //    model.Dates.Add(result.TimeSlots.Where(ts => ts.DateTime.Date == date).Select(ts => new EventModel.DatesModel()
                //         {
                //             Date = ts.DateTime.Date,
                //             Times = ts.Select(ts => ts.DateTime.ToShortTimeString()).ToList()
                //                }).ToList())
                //}
            }

            return model;
        }
        private static List<EventModel.DatesModel> GetDefaultDatesModel()
        {
            return new List<EventModel.DatesModel>()
            {
                new EventModel.DatesModel()
                {
                    Date = DateTime.Now,
                    Times = new List<string>()
                    {
                        "00:00"
                    }
                }
            };
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

        private async Task PopulateVenueDetails(EventModel eventModel)
        {
            var venuesDetails = await _placeService.GetPlacesDetailsAsync(eventModel.Places.Select(p => p.VenueId).ToList());
            eventModel.Places = eventModel.Places.Select(p => venuesDetails.Single(venue => venue.VenueId == p.VenueId)).ToList();
        }
    }
}