
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public class Role : EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
