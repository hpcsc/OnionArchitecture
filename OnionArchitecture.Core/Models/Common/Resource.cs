
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public class Resource : EntityBase<int>
    {
        private string Name { get; set; }
        private string Description { get; set; }

        private int? ParentId { get; set; }
        private IList<Resource> Children { get; set; }
        private IList<Permission> Permissions { get; set; }

        public ResourceSnapshot GetSnapshot()
        {
            return new ResourceSnapshot
            {
                Id = Id,
                Name = Name,
                Description = Description
            };
        }
    }
}
