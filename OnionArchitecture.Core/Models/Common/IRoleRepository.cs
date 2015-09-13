using System.Collections;
using System.Collections.Generic;
using OnionArchitecture.Core.Infrastructure.Repositories;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IRoleRepository
    {
        IList<Role> FindAll();
        IList<Role> FindRolesForUser(User user);
    }
}
