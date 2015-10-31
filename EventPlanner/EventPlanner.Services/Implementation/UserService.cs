using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class UserService : IUserService
    {
        public Task<List<Event>> GetEventsCreatedBy(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEventEditableFor(Guid eventId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}