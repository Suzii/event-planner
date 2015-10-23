using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Models;

namespace EventPlanner.Services
{
    /// <summary>
    /// 
    /// </summary>
    interface IPlaceService
    {
        /// <summary>
        /// Get suggestions for given query and city in async operation
        /// </summary>
        /// <param name="term">Part of the place name</param>
        /// <param name="city">City in which to look for</param>
        /// <param name="maxCount">Maximum count of suggestions</param>
        /// <returns><c ref="List of FourSquareVenueModel"/> containg required suggestions</returns>
        Task<List<FourSquareVenueModel>> GetPlaceSuggestionsAsync(string term, string city, int maxCount);

        //Task<FourSquareVenueModel> GetPlaceDetailAsync(string id);
    }
}
