using System.Collections.Generic;

namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    ///  Venue that only contain id, name, location, and categories.
    /// </summary>
    public class MiniVenue
    {
        /// <summary>
        /// ID of venue
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of venue
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location of venue
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Categories of venue
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}