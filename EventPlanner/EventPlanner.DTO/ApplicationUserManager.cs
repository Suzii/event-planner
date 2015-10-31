using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EventPlanner.Entities
{
    class ApplicationUserManager : UserManager<UserEntity>
    {
        public ApplicationUserManager(IUserStore<UserEntity> store)
            : base(store)
        {
        }
    }
}
    