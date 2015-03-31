
using System.Collections.Generic;
namespace OnionArchitecture.Core.Models.Common
{
    public class Resource : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public virtual ICollection<Resource> Children { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
