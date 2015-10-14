using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models.Domain
{
    class TimeSlot
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public DateTime DateTime { set; get; }
    }
}
