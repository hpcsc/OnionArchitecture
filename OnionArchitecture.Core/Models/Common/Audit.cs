using System;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public class Audit : EntityBase<int>
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }

        public virtual ICollection<AuditedValue> AuditedValues { get; set; }
    }
}
