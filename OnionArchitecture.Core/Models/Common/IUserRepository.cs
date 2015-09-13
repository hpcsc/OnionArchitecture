using OnionArchitecture.Core.Infrastructure.Repositories;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IUserRepository
    {
        User FindByUsername(string username);
    }
}
