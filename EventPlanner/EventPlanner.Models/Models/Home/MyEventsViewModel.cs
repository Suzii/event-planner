using System.Collections.Generic;
using EventPlanner.Models.Models.Shared;

namespace EventPlanner.Models.Models.Home
{
    public class MyEventsViewModel
    {
        public MyEventsViewModel()
        {
            Events = new List<EventInfoViewModel>();
        }

        public IList<EventInfoViewModel> Events { get; set; }
    }
}
