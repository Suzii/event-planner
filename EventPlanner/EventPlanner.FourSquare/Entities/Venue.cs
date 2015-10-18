using System.Collections.Generic;

namespace EventPlanner.FourSquare.Entities
{
    public class Venue
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Contact Contact { get; set; }

        public Location Location { get; set; }

        public List<Category> Categories { get; set; }

        public bool Verified { get; set; }

        public Stats Stats { get; set; }

        public string Url { get; set; }

        public string CanonicalUrl { get; set; }
    }
}