using OnionArchitecture.Core.Models.Common;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(IDbContext context)
            :base(context)
        {            
        }
    }
}
