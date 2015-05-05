using OnionArchitecture.Core.Models.Common;
using System.Data.Entity.ModelConfiguration;

namespace OnionArchitecture.Repository.EntityFramework.Mapping
{
    public class AuditedValueMap : EntityTypeConfiguration<AuditedValue>
    {
        public AuditedValueMap()
        {
            HasKey(p => new { p.AuditId, p.Name });
        }
    }
}
