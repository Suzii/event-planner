using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models.Domain
{
    class Place
    {
        public Guid Id { set; get; }
        public Guid EventId { set; get; }
        public Guid VenueId { set; get; }
    }
}
