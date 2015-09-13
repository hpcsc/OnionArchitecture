using System.Collections;
using OnionArchitecture.Core.Infrastructure.Repositories;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Models.Common
{
    public interface IResourceRepository
    {
        IEnumerable<Resource> GetResourceHierarchy();
        Resource FindBy(int resourceId);
    }
}
