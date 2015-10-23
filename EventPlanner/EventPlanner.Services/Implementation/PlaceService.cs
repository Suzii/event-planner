using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.FourSquare.Utils;
using EventPlanner.Models.Models;

namespace EventPlanner.Services.Implementation
{
    public class PlaceService : IPlaceService
    {
        public async Task<List<FourSquareVenueModel>> GetFourSquareVenueModelList(string term, string city)
        {
            var venueModels = new List<FourSquareVenueModel>();
            var fs = new FoursquareProvider();

            var suggestResponse = await fs.GetVenueSuggestionsAsync(term, city);

            foreach (var miniVenue in suggestResponse.Response.MiniVenues)
            {
                var model = new FourSquareVenueModel
                {
                    AddressInfo = miniVenue.Location.Address,
                    //Category = miniVenue.Categories.Find(x => x.Primary).Name,
                    City = miniVenue.Location.City,
                    Name = miniVenue.Name,
                    VenueId = miniVenue.Id
                };

                venueModels.Add(model);
            }

            return venueModels;
        }
    }
}