using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using EventPlanner.FourSquare.Utils;
using EventPlanner.Models.Models.Shared;
using EventPlanner.FourSquare.Entities;
using System.Diagnostics;

namespace EventPlanner.Services.Implementation.Tests
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

            response.Response = new MiniVenuesObject { MiniVenues = venues};
            response.Meta = new Meta { Code = 200 };
            tcs.SetResult(response);

            _provider.Setup(mock => mock.GetVenueSuggestionsAsync("pizza", "Brno", 30)).Returns(tcs.Task);
            var suggestionTask = await _placeService.GetPlaceSuggestionsAsync("pizza", "Brno", 30);
            _provider.Verify(mock => mock.GetVenueSuggestionsAsync("pizza", "Brno", 30), Times.Once);

            List<FourSquareVenueModel> venueModels = suggestionTask.ToList();
            Assert.IsTrue(venues.Count > 0 && venues.Count <= 30);
            foreach (FourSquareVenueModel v in venueModels)
            {
                Assert.IsTrue(v.City == null || v.City.Contains("Brno"));
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
            _provider.Verify(mock => mock.GetVenueAsync("1"), Times.Once);
            

            Assert.AreEqual(v.Id, task.VenueId);
            Assert.AreEqual(v.Name, task.Name);
            Assert.AreEqual(v.Location.Address, task.AddressInfo);
            Assert.AreEqual(v.Location.City, task.City);
            Assert.AreEqual(v.Location.Lat, task.Lat);
            Assert.AreEqual(v.Location.Lng, task.Lng);
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
            _provider.Verify(mock => mock.GetVenueAsync("1"), Times.Once);
            _provider.Verify(mock => mock.GetVenueAsync("2"), Times.Once);

            Assert.AreEqual(v.Id, task[0].VenueId);
            Assert.AreEqual(v.Name, task[0].Name);
            Assert.AreEqual(v.Location.Address, task[0].AddressInfo);
            Assert.AreEqual(v.Location.City, task[0].City);
            Assert.AreEqual(v.Location.Lat, task[0].Lat);
            Assert.AreEqual(v.Location.Lng, task[0].Lng);

            Assert.AreEqual(v2.Id, task[1].VenueId);
            Assert.AreEqual(v2.Name, task[1].Name);
            Assert.AreEqual(v2.Location.Address, task[1].AddressInfo);
            Assert.AreEqual(v2.Location.City, task[1].City);
            Assert.AreEqual(v2.Location.Lat, task[1].Lat);
            Assert.AreEqual(v2.Location.Lng, task[1].Lng);
        }
    }
}