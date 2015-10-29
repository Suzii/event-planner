using System;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// crud operations on events, handling logic
    /// </summary>
    public interface IEventManagementService
    {
        Event CreateEvent(Event e);

        Event UpdateEvent(Event e);

        /// <summary>
        /// V ostatních metodách nevracet Eventy, které jsou disabled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DisableEvent(Guid id);

        Event GetEvent(Guid id);

        Guid GetEventId(string eventHash);
    }
}
