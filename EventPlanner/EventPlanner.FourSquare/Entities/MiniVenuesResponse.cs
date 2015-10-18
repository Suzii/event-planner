namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Class representing a response to Suggest Completion Foursquare API call
    /// </summary>
    public class MiniVenuesResponse
    {
        /// <summary>
        /// Meta informations about response
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public MiniVenuesObject Response { get; set; }
    }
}