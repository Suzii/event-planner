using System.Threading.Tasks;
using EventPlanner.FourSquare.Entities;

namespace EventPlanner.FourSquare.Utils
{
    public interface IFoursquareProvider
    {
        Task<MiniVenuesResponse> GetVenueSuggestionsAsync(string suggestion, string city, int limit);
    }
}
