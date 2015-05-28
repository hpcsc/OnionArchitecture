using OnionArchitecture.Core.Infrastructure.Repositories;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
