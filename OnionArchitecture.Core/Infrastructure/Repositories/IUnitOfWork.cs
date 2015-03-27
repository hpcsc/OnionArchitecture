
namespace OnionArchitecture.Core.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
        void Dispose();
        void Dispose(bool disposing);
    }
}
