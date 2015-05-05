using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Infrastructure.Auditing
{
    public interface IAuditor<T>
    {
        List<AuditedValue> Audit(T entity);
    }
}
