using OnionArchitecture.Core.Infrastructure.Repositories;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IResourceRepository : IRepository<Resource>
    {
        IEnumerable<Resource> GetResourceHierarchy();
    }
}
