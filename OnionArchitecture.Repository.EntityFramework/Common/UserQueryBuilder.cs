using System.Data.Entity;
using OnionArchitecture.Repository.EntityFramework.PersistenceModel;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class UserQueryBuilder : QueryBuilderBase<UserPersistenceModel>
    {
        public UserQueryBuilder(DbContext context)
            :base(context)
        {            
        }
    }
}
