using System.Threading.Tasks;
using EventPlanner.FourSquare.Entities;

namespace EventPlanner.FourSquare.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFoursquareProvider
    {
        /// <summary>
        /// Get list of mini venues matching the search term, near the location in an asynchronous operation
        /// </summary>
        /// <param name="term">Search term to be applied</param>
        /// <param name="city">Search nearby</param>
        /// <param name="limit">Maximal number of results</param>
        /// <returns><c ref="MiniVenuesResponse" /> containing list of mini venues</returns>
        Task<MiniVenuesResponse> GetVenueSuggestionsAsync(string term, string city, int limit);

        /// <summary>
        /// Get complete venue by given ID in an asynchronous operation
        /// </summary>
        /// <param name="id">ID of venue</param>
        /// <returns><c ref="VenueResponse" /> containing complete venue</returns>
        Task<VenueResponse> GetVenueAsync(string id);
    }
}
