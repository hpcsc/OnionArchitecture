using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(IDbContext context)
            :base(context)
        {            
        }
    }
}
