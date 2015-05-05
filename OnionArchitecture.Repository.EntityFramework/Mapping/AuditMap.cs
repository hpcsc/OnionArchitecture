using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    public class AuditMap : EntityBaseMap<Audit>
    {
        public AuditMap()
        {
            HasMany<AuditedValue>(p => p.AuditedValues).WithRequired().HasForeignKey(p => p.AuditId);
        }
    }
}
