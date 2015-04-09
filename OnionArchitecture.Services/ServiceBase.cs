using OnionArchitecture.Core.Infrastructure.Caching;
using OnionArchitecture.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionArchitecture.Services
{
    /*
     * To use protected properties, need to register concrete service with PropertiesAutowired() in Autofac registration
     */
    public abstract class ServiceBase
    {
        public ICacheStore CacheStore { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IResourceRepository ResourceRepository { get; set; }

        protected bool UserHasAccessToResource(int userId, string resourceFullName, PermissionType accessType)
        {
            var cacheKey = string.Format("ServiceBase:UserHasAccessToResource:{0}", userId);
            var resourceHierarchy = LookUpResourceHierarchy(resourceFullName);

            var user = GetFromCache(cacheKey, () => UserRepository.FindBy(userId,
                        u => u.Permissions,
                        u => u.Roles.Select(r => r.Permissions)));

            return user.HasAccessTo(resourceHierarchy.Last().Id, accessType);
        }

        protected bool RoleHasAccessToResource(int roleId, string resourceFullName, PermissionType accessType)
        {
            var cacheKey = string.Format("ServiceBase:RoleHasAccessToResource:{0}", roleId);
            var resourceHierarchy = LookUpResourceHierarchy(resourceFullName);

            var role = GetFromCache(cacheKey, () => RoleRepository.FindBy(roleId, r => r.Permissions));

            return role.HasAccessTo(resourceHierarchy.Last().Id, accessType);
        }

        protected T GetFromCache<T>(string cacheKey, Func<T> updateFunc) where T : class
        {
            var entity = CacheStore.Retrieve<T>(cacheKey);
            if (entity == null)
            {
                entity = updateFunc();

                CacheStore.Store(cacheKey, entity);
            }

            return entity;
        }

        protected List<Resource> LookUpResourceHierarchy(string resourceFullName)
        {
            var resources = GetFromCache<List<Resource>>("ResourceHierarchy", () => ResourceRepository.GetResourceHierarchy().ToList());

            var resourceNames = resourceFullName.Split('.');

            var currentResources = resources;
            var result = new List<Resource>();
            for (int i = 0; i < resourceNames.Length; i++)
            {
                var resource = currentResources.FirstOrDefault(r => r.Name.Equals(resourceNames[i], StringComparison.InvariantCultureIgnoreCase));
                if(resource == null)
                {
                    throw new ApplicationException(string.Format("Resource with name '{0}' in not found", resourceNames[i]));
                }

                result.Add(resource);

                currentResources = resource.Children.ToList();
            }

            return result;
        }
    }
}
