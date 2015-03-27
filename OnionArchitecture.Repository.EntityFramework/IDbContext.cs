using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OnionArchitecture.Repository.EntityFramework
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry Entry<T>(T entity) where T : class;
        int Commit();
        void Dispose();
    }
}
