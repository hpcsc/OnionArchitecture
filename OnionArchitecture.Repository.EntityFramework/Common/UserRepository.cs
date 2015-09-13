using System.Data.Entity;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(DbContext context)
            :base(context)
        {            
        }

        public User FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
