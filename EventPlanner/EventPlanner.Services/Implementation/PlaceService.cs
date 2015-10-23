using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.FourSquare.Utils;
using EventPlanner.Models.Models;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class provide services related to places. Search, get details, ...
    /// </summary>
    public class PlaceService : IPlaceService
    {
        private readonly IFoursquareProvider _fs;
        
        /// <summary>
        /// Initialize new instance of PlaceService()
        /// </summary>
        public PlaceService()
        {
            _fs = new FoursquareProvider();
        }

        /// <summary>
        /// Get Place suggestions in async operation
        /// </summary>
        /// <param name="term">Part of the place name</param>
        /// <param name="city">City in which to look for</param>
        /// <param name="maxCount">Maximum count of suggestions</param>
        /// <returns><c ref="List of FourSquareVenueModel"/> containg required suggestions</returns>
        public async Task<List<FourSquareVenueModel>> GetPlaceSuggestionsAsync(string term, string city, int maxCount = 30)
        {
            var venueModels = new List<FourSquareVenueModel>();

            var suggestionsResponse = await _fs.GetVenueSuggestionsAsync(term, city, maxCount);
            if (suggestionsResponse.Meta.Code != 200)
            {
                return null;
            }

            venueModels.AddRange(suggestionsResponse.Response.MiniVenues.Select(miniVenue => new FourSquareVenueModel
            {
                AddressInfo = miniVenue.Location.Address,
                City = miniVenue.Location.City,
                Name = miniVenue.Name,
                VenueId = miniVenue.Id
            }));

            return venueModels;
        }

        public Task<FourSquareVenueModel> GetPlaceDetailAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<FourSquareVenueModel>> GetPlacesDetailsAsync(IList<string> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}