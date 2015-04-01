using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext context)
            : base(context)
        {

        }
    }
}
