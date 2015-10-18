using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using EventPlanner.FourSquare.Entities;
using System.Threading.Tasks;

namespace EventPlanner.FourSquare.Utils
{
    /// <summary>
    /// Class providing basic foursquare operations
    /// </summary>
    public class FoursquareProvider : IFoursquareProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;

        private const string URI_BASE = "https://api.foursquare.com/v2/venues/";
        private const string SUGGEST_COMPLETION = "suggestCompletion?";
        private const string CLIENT_ID = "&client_id={0}";
        private const string CLIENT_SECRET = "&client_secret={0}";
        private const string NEAR = "&near={0}";
        private const string QUERY = "&query={0}";
        private const string LIMIT = "&limit={0}";
        private const string VERSION = "&v=20151110";
        private const string MODE = "&m=foursquare";

        /// <summary>
        /// Initialize new instance of FoursquareProvider
        /// </summary>
        public FoursquareProvider()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            _httpClient = new HttpClient(handler);
            _clientId = ConfigurationManager.AppSettings["ClientId"];
            _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        }

        /// <summary>
        /// Returns a list of mini-venues partially matching the search term, near the location in an asynchronous operation
        /// </summary>
        /// <param name="term">A search term to be applied against titles. Must be at least 3 characters long.</param>
        /// <param name="location">A string naming a place in the world</param>
        /// <param name="limit">Number of results to return</param>
        /// <exception cref="ArgumentOutOfRangeException">If term is not at least 3 characters long</exception>
        /// <exception cref="ArgumentNullException">If location is null or empty</exception>
        /// <exception cref="HttpRequestException">If location could not be found</exception>
        /// <returns><c ref="MiniVenuesResponse" /> which contain Meta information about status code of the response and list of mini-venues</returns>
        public async Task<MiniVenuesResponse> GetVenueSuggestionsAsync(string term, string location, int limit = 30)
        {
            if (term.Length < 3)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(term));
            }

            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(paramName: nameof(location));
            }

            var invoker = WebServiceInvoker<MiniVenuesResponse>.Create(_httpClient, new Uri(
                    URI_BASE +
                    SUGGEST_COMPLETION +
                    string.Format(CLIENT_ID, _clientId) +
                    string.Format(CLIENT_SECRET, _clientSecret) +
                    string.Format(NEAR, location) +
                    string.Format(QUERY, term) +
                    string.Format(LIMIT, limit) +
                    VERSION +
                    MODE
                ));
            return await invoker.InvokeWebService();
        }

        /// <summary>
        /// Returns a complete venue matching given id in an asynchronous operation
        /// </summary>
        /// <param name="id">ID of venue to retrieve</param>
        /// <exception cref="ArgumentNullException">If id is null or empty</exception>
        /// <returns><c ref="VenueResponse" /> which contain Meta information about status code of the response and a complete venue</returns>
        public async Task<VenueResponse> GetVenueAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(paramName: nameof(id));
            }

            var invoker = WebServiceInvoker<VenueResponse>.Create(_httpClient, new Uri(
                    URI_BASE +
                    id + "?" +
                    string.Format(CLIENT_ID, _clientId) +
                    string.Format(CLIENT_SECRET, _clientSecret) +
                    VERSION +
                    MODE
                ));
            return await invoker.InvokeWebService();
        }
    }
}