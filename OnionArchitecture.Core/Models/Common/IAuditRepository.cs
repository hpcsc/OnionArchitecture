using OnionArchitecture.Core.Infrastructure.Repositories;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IAuditRepository : IRepository<Audit, int>
    {
    }
}
