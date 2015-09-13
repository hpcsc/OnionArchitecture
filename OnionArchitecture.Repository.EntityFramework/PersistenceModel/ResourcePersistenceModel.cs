using System.Collections.Generic;

namespace OnionArchitecture.Repository.EntityFramework.PersistenceModel
{
    internal class ResourcePersistenceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public IList<ResourcePersistenceModel> Children { get; set; }
        public IList<PermissionPersistenceModel> Permissions { get; set; }
    }
}
