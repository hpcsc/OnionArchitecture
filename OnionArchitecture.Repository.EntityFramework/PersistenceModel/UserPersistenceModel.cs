using System.Collections.Generic;
using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.PersistenceModel
{
    public class UserPersistenceModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserStatus Status { get; set; }

        public IList<RolePersistenceModel> Roles { get; set; }
        public IList<PermissionPersistenceModel> Permissions { get; set; }
    }
}
