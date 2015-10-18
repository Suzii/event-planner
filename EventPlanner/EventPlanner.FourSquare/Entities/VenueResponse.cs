namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Class representing a response to Venue detail Foursquare API call
    /// </summary>
    public class VenueResponse
    {
        /// <summary>
        /// Meta informations about response
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public VenueObject Response { get; set; }
    }
}