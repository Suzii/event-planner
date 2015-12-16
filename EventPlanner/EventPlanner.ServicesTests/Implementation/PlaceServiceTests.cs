using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.FourSquare.Entities;
using EventPlanner.FourSquare.Utils;
using EventPlanner.Models.Models.Shared;
using EventPlanner.Services.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EventPlanner.ServicesTests.Implementation
{
    [TestClass()]
    public class PlaceServiceTests
    {
        private PlaceService _placeService;
        private Mock<IFoursquareProvider> _provider;

        [TestInitialize()]
        public void TestInitialize()
        {
            _provider = new Mock<IFoursquareProvider>();
            _placeService = new PlaceService(_provider.Object);
        }

        [TestMethod()]
        public async Task GetPlaceSuggestionsAsyncTest()
        {
            var tcs = new TaskCompletionSource<MiniVenuesResponse>();
            MiniVenuesResponse response = new MiniVenuesResponse();
            List<MiniVenue> venues = new List<MiniVenue>();
            Location location1 = new Location { Address = "Some random address", City = "Brno", Lat = 49, Lng = 13 };
            Location location2 = new Location { Address = "Botanická 834/56, 602 00 Brno-Veveří", City = "Brno, Czech Republic", Lat = 48, Lng = 13 };

            venues.Add(new MiniVenue { Id = "0", Name = "Blablabla", Location = location1 });
            venues.Add(new MiniVenue { Id = "1", Name = "Very special restaurant", Location = location2 });

            response.Response = new MiniVenuesObject { MiniVenues = venues };
            response.Meta = new Meta { Code = 200 };
            tcs.SetResult(response);

            _provider.Setup(mock => mock.GetVenueSuggestionsAsync("pizza", "Brno", 30)).Returns(tcs.Task);
            var suggestionTask = await _placeService.GetPlaceSuggestionsAsync("pizza", "Brno", 30);
            _provider.Verify(mock => mock.GetVenueSuggestionsAsync("pizza", "Brno", 30), Times.Once, "Method GetVenueSuggestionSync was not called or was called more than once (or its parameters were wrong).");

            List<FourSquareVenueModel> venueModels = suggestionTask.ToList();
            Assert.IsTrue(venues.Count > 0 && venues.Count <= 30, "The number of returned venues was different than expected.");
            foreach (FourSquareVenueModel v in venueModels)
            {
                Assert.IsTrue(v.City == null || v.City.Contains("Brno"), "If venue's city field is not null, it should contain 'Brno'");
            }
        }

        [TestMethod()]
        public async Task GetPlaceDetailAsyncTest()
        {
            var tcs = new TaskCompletionSource<VenueResponse>();
            VenueResponse response = new VenueResponse();
            Location location = new Location { Address = "2 Rue de la Hacquinière, 91440", City = "Bures-sur-Yvette, France", Lat = 45, Lng = 13 };
            Venue v = new Venue { Id = "1", Name = "La pâtisserie chez moi", Location = location };
            response.Response = new VenueObject { Venue = v };
            response.Meta = new Meta { Code = 200 };
            tcs.SetResult(response);

            _provider.Setup(mock => mock.GetVenueAsync("1")).Returns(tcs.Task);
            var task = await _placeService.GetPlaceDetailAsync("1");
            _provider.Verify(mock => mock.GetVenueAsync("1"), Times.Once, "Method GetVenueAsync was not called or was called more than once (or its parameters were wrong).");


            Assert.AreEqual(v.Id, task.VenueId, "Returned venue has different id.");
            Assert.AreEqual(v.Name, task.Name, "Returned venue has different name.");
            Assert.AreEqual(v.Location.Address, task.AddressInfo, "Returned venue has different address.");
            Assert.AreEqual(v.Location.City, task.City, "Returned venue has different city.");
            Assert.AreEqual(v.Location.Lat, task.Lat, "Returned venue has different latitude.");
            Assert.AreEqual(v.Location.Lng, task.Lng, "Returned venue has different longitude.");
        }

        [TestMethod()]
        public async Task GetPlacesDetailsAsyncTest()
        {
            var tcs = new TaskCompletionSource<VenueResponse>();
            VenueResponse response = new VenueResponse();
            Location location = new Location { Address = "2 Rue de la Hacquinière, 91440", City = "Bures-sur-Yvette, France", Lat = 45, Lng = 13 };
            Venue v = new Venue { Id = "1", Name = "La pâtisserie chez moi", Location = location };
            response.Response = new VenueObject { Venue = v };
            response.Meta = new Meta { Code = 200 };
            tcs.SetResult(response);

            var tcs2 = new TaskCompletionSource<VenueResponse>();
            VenueResponse response2 = new VenueResponse();
            Location location2 = new Location { Address = "SNP, 018 51", City = "Nová Dubnica", Lat = 48, Lng = 13 };
            Venue v2 = new Venue { Id = "2", Name = "Pizzeria Livigno", Location = location };
            response2.Response = new VenueObject { Venue = v2 };
            response2.Meta = new Meta { Code = 200 };
            tcs2.SetResult(response2);

            List<string> requestList = new List<string> { v.Id, v2.Id };

            _provider.Setup(mock => mock.GetVenueAsync("1")).Returns(tcs.Task);
            _provider.Setup(mock => mock.GetVenueAsync("2")).Returns(tcs2.Task);
            var task = await _placeService.GetPlacesDetailsAsync(requestList);
            _provider.Verify(mock => mock.GetVenueAsync("1"), Times.Once, "Method GetVenueAsync was not called or was called more than once (or its parameters were wrong).");
            _provider.Verify(mock => mock.GetVenueAsync("2"), Times.Once, "Method GetVenueAsync was not called or was called more than once (or its parameters were wrong).");

            Assert.AreEqual(v.Id, task[0].VenueId, "Returned venue has different id.");
            Assert.AreEqual(v.Name, task[0].Name, "Returned venue has different name.");
            Assert.AreEqual(v.Location.Address, task[0].AddressInfo, "Returned venue has different address.");
            Assert.AreEqual(v.Location.City, task[0].City, "Returned venue has different city.");
            Assert.AreEqual(v.Location.Lat, task[0].Lat, "Returned venue has different latitude.");
            Assert.AreEqual(v.Location.Lng, task[0].Lng, "Returned venue has different longitude.");

            Assert.AreEqual(v2.Id, task[1].VenueId, "Returned venue has different id.");
            Assert.AreEqual(v2.Name, task[1].Name, "Returned venue has different name.");
            Assert.AreEqual(v2.Location.Address, task[1].AddressInfo, "Returned venue has different address.");
            Assert.AreEqual(v2.Location.City, task[1].City, "Returned venue has different city.");
            Assert.AreEqual(v2.Location.Lat, task[1].Lat, "Returned venue has different latitude.");
            Assert.AreEqual(v2.Location.Lng, task[1].Lng, "Returned venue has different longitude.");
        }
    }
}