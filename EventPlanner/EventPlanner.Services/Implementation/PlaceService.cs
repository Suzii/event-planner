using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DAL.AutoMappers;
using EventPlanner.DAL.Repository;
using EventPlanner.FourSquare.Utils;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Models.Models.Vote;

namespace EventPlanner.Services.Implementation
{
    /// <summary>
    /// Class provide services related to places. Search, get details, ...
    /// </summary>
    public class PlaceService : IPlaceService
    {
        private readonly IFoursquareProvider _fs;

        private readonly PlaceRepository _placeRepository;
        
        /// <summary>
        /// Initialize new instance of PlaceService()
        /// </summary>
        public PlaceService() : this(new FoursquareProvider(), new PlaceRepository()) { }

        public PlaceService(IFoursquareProvider provider, PlaceRepository placeRepository)
        {
            _fs = provider;
            _placeRepository = placeRepository;
        }

        /// <summary>
        /// Get Place suggestions in async operation
        /// </summary>
        /// <param name="term">Part of the place name</param>
        /// <param name="city">City in which to look for</param>
        /// <param name="maxCount">Maximum count of suggestions</param>
        /// <returns><c ref="List of FourSquareVenueModel"/> containg required suggestions</returns>
        public async Task<IList<FourSquareVenueModel>> GetPlaceSuggestionsAsync(string term, string city, int maxCount = 30)
        {
            var venueModels = new List<FourSquareVenueModel>();

            var suggestionsResponse = await _fs.GetVenueSuggestionsAsync(term, city, maxCount);
            if (suggestionsResponse.Meta.Code != 200)
            {
                return null;
            }

            venueModels.AddRange(suggestionsResponse.Response.MiniVenues.Select(miniVenue => new FourSquareVenueModel
            {
                AddressInfo = miniVenue.Location.Address ?? "Unknown",
                City = miniVenue.Location.City,
                Name = miniVenue.Name,
                VenueId = miniVenue.Id,
                Lat = miniVenue.Location.Lat,
                Lng = miniVenue.Location.Lng
            }));

            return venueModels;
        }

        /// <summary>
        /// Get Place detail informations in async operation
        /// </summary>
        /// <param name="id">ID of wanted place</param>
        /// <returns><c ref="FourSquareVenueModel"/> containing place detail</returns>
        public async Task<FourSquareVenueModel> GetPlaceDetailAsync(string id)
        {
            var detailResponse = await _fs.GetVenueAsync(id);

            if (detailResponse.Meta.Code != 200)
            {
                return null;
            }

            return new FourSquareVenueModel()
            {
                AddressInfo = detailResponse.Response.Venue.Location.Address ?? "Unknown",
                City = detailResponse.Response.Venue.Location.City,
                Name = detailResponse.Response.Venue.Name,
                VenueId = detailResponse.Response.Venue.Id,
                Lat = detailResponse.Response.Venue.Location.Lat,
                Lng = detailResponse.Response.Venue.Location.Lng
            };
        }

        /// <summary>
        /// Get Places details informations in async operation
        /// </summary>
        /// <param name="ids">List of IDs of wanted places</param>
        /// <returns>List of ><c ref="FourSquareVenueModel"/> containing places details</returns>
        public async Task<IList<FourSquareVenueModel>> GetPlacesDetailsAsync(IList<string> ids)
        {
            //TODO: ids.Select(async id => await GetPlaceDetailAsync(id)).Where(pl => pl != null).ToList();
            var detailModels = new List<FourSquareVenueModel>();
            foreach (var id in ids)
            {
                var detail = await GetPlaceDetailAsync(id);
                if (detail != null)
                {
                    detailModels.Add(detail);
                }
            }
            return detailModels;
        }

        public async Task PopulateVenueDetailsAsync(IList<PlaceViewModel> places)
        {
            var venuesDetails = await GetPlacesDetailsAsync(places.Select(p => p.VenueId).ToList());
            foreach (var place in places)
            {
                place.Venue = venuesDetails.Single(p => p.VenueId == place.VenueId);
            }
        }
    }
}