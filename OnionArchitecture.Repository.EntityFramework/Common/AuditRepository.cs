using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class AuditRepository : RepositoryBase<Audit, int>, IAuditRepository
    {
        public AuditRepository(IDbContext context)
            :base(context)
        {
        }
    }
}
