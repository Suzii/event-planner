using System;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// crud operations on events, handling logic
    /// </summary>
    public interface IEventManagementService
    {
        Task<Event> CreateEvent(Event e);

        Task<Event> UpdateEvent(Event e);

        /// <summary>
        /// V ostatních metodách nevracet Eventy, které jsou disabled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DisableEvent(Guid id);

        Task<Event> GetEvent(Guid id);

        Task<Guid> GetEventId(string eventHash);
    }
}
