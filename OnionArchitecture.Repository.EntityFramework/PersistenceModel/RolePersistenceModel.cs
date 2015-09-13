using System.Collections.Generic;

namespace OnionArchitecture.Repository.EntityFramework.PersistenceModel
{
    internal class RolePersistenceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<PermissionPersistenceModel> Permissions { get; set; }
    }
}
