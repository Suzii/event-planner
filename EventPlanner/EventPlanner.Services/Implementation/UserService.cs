using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services.Implementation
{
    public class UserService : IUserService
    {
        public Task<List<EventEntity>> GetEventsCreatedBy(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEventEditableFor(Guid eventId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}