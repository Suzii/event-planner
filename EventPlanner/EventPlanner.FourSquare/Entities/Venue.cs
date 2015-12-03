using System.Collections.Generic;

namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Complete venue informations
    /// </summary>
    public class Venue
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
        /// Contact informations
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Location of venue
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// List of categories applied to this venue
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Flag indicating whether the owner of this business has claimed it and verified the information.
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// User statistics
        /// </summary>
        public Stats Stats { get; set; }

        /// <summary>
        /// URL of the venue's website, typically provided by the venue manager.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The canonical URL for this venue, e.g. "https://foursquare.com/v/foursquare-hq/4ab7e57cf964a5205f7b20e3"
        /// </summary>
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// Opening hours of this venue
        /// </summary>
        public Hours Hours { get; set; }
    }
}