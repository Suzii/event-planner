using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models.Domain
{
    class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public float ExpectedLength { get; set; }
        public Guid OrganizerId { get; set; }
        public bool OthersCanEdit { get; set; }
        public DateTime Created { get; set; }
/*        public int Hash
        {
            get
            {
                return Id ??? Created
            }
        }*/
    }
}
