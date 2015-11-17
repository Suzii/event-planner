using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Models;
using EventPlanner.Models.Models.Shared;

namespace EventPlanner.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlaceService
    {
        /// <summary>
        /// Get suggestions for given query and city in async operation
        /// </summary>
        /// <param name="term">Part of the place name</param>
        /// <param name="city">City in which to look for</param>
        /// <param name="maxCount">Maximum count of suggestions</param>
        /// <returns><c ref="List of FourSquareVenueModel"/> containing required suggestions</returns>
        Task<IList<FourSquareVenueModel>> GetPlaceSuggestionsAsync(string term, string city, int maxCount = 30);

        Task<FourSquareVenueModel> GetPlaceDetailAsync(string id);

        Task<IList<FourSquareVenueModel>> GetPlacesDetailsAsync(IList<string> ids);
    }
}
