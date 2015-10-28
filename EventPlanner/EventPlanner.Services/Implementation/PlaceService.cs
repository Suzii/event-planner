﻿using System.Collections.Generic;
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
                AddressInfo = miniVenue.Location.Address,
                City = miniVenue.Location.City,
                Name = miniVenue.Name,
                VenueId = miniVenue.Id
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
                AddressInfo = detailResponse.Response.Venue.Location.Address,
                City = detailResponse.Response.Venue.Location.City,
                Name = detailResponse.Response.Venue.Name,
                VenueId = detailResponse.Response.Venue.Id
            };
        }

        /// <summary>
        /// Get Places details informations in async operation
        /// </summary>
        /// <param name="ids">List of IDs of wanted places</param>
        /// <returns>List of ><c ref="FourSquareVenueModel"/> containing places details</returns>
        public async Task<IList<FourSquareVenueModel>> GetPlacesDetailsAsync(IList<string> ids)
        {
            var detailModels = new List<FourSquareVenueModel>();
            foreach (var id in ids)
            {
                var detailResponse = await _fs.GetVenueAsync(id);

                if (detailResponse.Meta.Code != 200)
                {
                    continue;
                }
                detailModels.Add(
                    new FourSquareVenueModel()
                    {
                        AddressInfo = detailResponse.Response.Venue.Location.Address,
                        City = detailResponse.Response.Venue.Location.City,
                        Name = detailResponse.Response.Venue.Name,
                        VenueId = detailResponse.Response.Venue.Id
                    });
            }
            return detailModels;
        }
    }
}