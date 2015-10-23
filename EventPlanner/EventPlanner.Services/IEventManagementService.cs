using System;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    /// <summary>
    /// crud operations on events, handling logic
    /// </summary>
    interface IEventManagementService
    {
        EventEntity CreateEvent(EventEntity e);

        EventEntity UpdateEvent(EventEntity e);

        /// <summary>
        /// V ostatních metodách nevracet Eventy, které jsou disabled
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DisableEvent(Guid id);

        EventEntity GetEntity(Guid id);
    }
}
