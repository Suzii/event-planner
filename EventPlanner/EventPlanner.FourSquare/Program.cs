using System;
using System.Linq;
using System.Net.Http;
using EventPlanner.FourSquare.Utils;
using EventPlanner.FourSquare.Entities;

namespace EventPlanner.FourSquare
{
    class Program
    {
        /// <summary>
        /// Just for development purposes to test and demonstrate functionality. Will be deleted.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var fs = new FoursquareProvider();

            while (true)
            {
                Console.WriteLine("Enter City");
                var city = Console.ReadLine();
                Console.WriteLine("Write suggestion");
                var suggestion = Console.ReadLine();
                
                /*var task = fs.GetVenueAsync("4ba8d9e0f964a52017f439e3");
                var r = task.Result;

                Venue v = r.Response.Venue;
                Console.WriteLine("Result: " + v.Hours);*/

                var suggestionTask = fs.GetVenueSuggestionsAsync(suggestion, city);
                try
                {
                    // Wait until task is completed
                    suggestionTask.Wait();
                    // Get result
                    var rv = suggestionTask.Result;
                    if (rv.Meta.Code == 200)
                    {
                        foreach (var miniVenue in rv.Response.MiniVenues)
                        {
                            Console.WriteLine(
                                miniVenue.Name + ", " + miniVenue.Location.Address );
                        }
                    }
                }
                catch (AggregateException ae)
                {
                    ae.Handle(x =>
                    {
                        // HttpClient throws HttpRequestException, when status code => 400
                        // Foursquare api returns code 400 when city was not found
                        // https://developer.foursquare.com/overview/responses
                        // This we know how to handle.
                        if (x is HttpRequestException) 
                        {
                            Console.WriteLine("City was not found");
                            return true;
                        }
                        if (x is ArgumentOutOfRangeException)
                        {
                            Console.WriteLine(x.Message);
                            return true;
                        }
                        // Let anything else stop the application.
                        return false; 
                    });
                }
            }
        }
    }
}
