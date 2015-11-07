using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventPlanner.FourSquare.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.FourSquare.Entities;
using System.Diagnostics;

namespace EventPlanner.FourSquare.Utils.Tests
{
    [TestClass()]
    public class FoursquareProviderTests
    {
        private IFoursquareProvider _fsProvider;

        [TestInitialize()]
        public void TestInitialize()
        {
            _fsProvider = new FoursquareProvider();
        }

        [TestMethod()]
        public void GetVenueSuggestionsAsyncTest()
        {
            var suggestionTask = _fsProvider.GetVenueSuggestionsAsync("pizza", "Brno", 30);
            suggestionTask.Wait();
            List<MiniVenue> venues = suggestionTask.Result.Response.MiniVenues;
            Assert.IsTrue(venues.Count > 0 && venues.Count <= 30);
            foreach (MiniVenue v in venues)
            {
                Debug.WriteLine(v.Location.City);
                Assert.IsTrue(v.Location.City == null || v.Location.City.Contains("Brno"));
            }
        }

        [TestMethod()]
        public void GetVenueAsyncTest()
        {
            var suggestionTask = _fsProvider.GetVenueSuggestionsAsync("pizza", "Brno", 30);
            suggestionTask.Wait();
            List<MiniVenue> venues = suggestionTask.Result.Response.MiniVenues;

            var task = _fsProvider.GetVenueAsync(venues[0].Id);
            task.Wait();
            Venue venue = task.Result.Response.Venue;
            Assert.AreEqual(venues[0].Id, venue.Id);
            Assert.AreEqual(venues[0].Name, venue.Name);
            Assert.AreEqual(venues[0].Location, venue.Location);
            CollectionAssert.AreEqual(venues[0].Categories, venue.Categories);
        }
    }
}