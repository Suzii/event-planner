namespace EventPlanner.Models.Models.Vote
{
    /// <summary>
    /// This class serves as ViewModel for Place option
    /// that is serialized to JSON object and passed to JavaScript component,
    /// primarily designed for Google Maps module.
    /// 
    /// </summary>
    public class PlaceMapViewModel
    {
        public string VenueId { set; get; }

        public string Name { get; set; }

        public string City { get; set; }

        public string AddressInfo { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}
