using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Models;

namespace EventPlanner.Services
{
    interface IPlaceService
    {
        Task<List<FourSquareVenueModel>> GetFourSquareVenueModelList(string term, string city);

        //Task<FourSquareVenueModel> GetPlaceDetailAsync(string id);
    }
}
