using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Models.CreateAndEdit;

namespace EventPlanner.DAL.AutoMappers
{
    public static class MappingHelper
    {
        public static IList<TimeSlot> MapToTimeSlot(IList<EventModel.DatesModel> dates)
        {
            return dates.SelectMany(d => d.Times.Select(time => new TimeSlot()
            {
                Id = time.Id,
                DateTime = d.Date.Add(TimeSpan.Parse(time.Time))
            }).ToList()).ToList();
        }

        public static IList<EventModel.DatesModel> MapToDatesModel(IList<TimeSlot> timeSlots)
        {
            return timeSlots.GroupBy(ts => ts.DateTime.Date, ts => ts)
                .Select(
                    tsGrp =>
                        new EventModel.DatesModel()
                        {
                            Date = tsGrp.Key.Date,
                            Times = tsGrp.Select(ts => new EventModel.TimeModel(ts.Id, ts.DateTime.ToString("HH:mm:ss"))).ToList()
                        }).ToList();
        }
    }
}
