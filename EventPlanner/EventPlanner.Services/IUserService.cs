using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IUserService
    {
        Task<IList<Event>> GetEventsCreatedBy(string userId);

        Task<bool> IsEventEditableFor(Guid eventId, string userId);
    }
}
