using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IUserService
    {
        Task<List<EventEntity>> GetEventsCreatedBy(Guid userId);

        Task<bool> IsEventEditableFor(Guid eventId, Guid userId);
    }
}
