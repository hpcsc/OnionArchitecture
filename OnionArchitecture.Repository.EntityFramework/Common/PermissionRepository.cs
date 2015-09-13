using System.Data.Entity;
using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class PermissionRepository : RepositoryBase<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(DbContext context)
            : base(context)
        {

        }
    }
}
