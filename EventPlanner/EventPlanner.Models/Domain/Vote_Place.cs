using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models.Domain
{
    class Vote_Place
    {
        public Guid Id { set; get; }
        public Guid UserId { set; get; }
        public Guid PlaceId { set; get; }
    }
}
