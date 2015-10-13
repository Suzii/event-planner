using System.Collections.Generic;

namespace EventPlanner.FourSquare.Entities
{
    public class MiniVenue
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public List<Category> Categories { get; set; }
    }
}