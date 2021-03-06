﻿using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;
using System.Linq;

namespace OnionArchitecture.Repository.EntityFramework.Common
{
    public class ResourceRepository : RepositoryBase<Resource, int>, IResourceRepository
    {
        public ResourceRepository(IDbContext context) : 
            base(context)
        {
        }

        public IEnumerable<Resource> GetResourceHierarchy()
        {
            var resources = FindBy(r => !r.ParentId.HasValue, r => r.Children).ToList();

            var secondLevelChildren = resources.SelectMany(r => r.Children);

            LoadChildren(resources);

            return resources;
        }

        private void LoadChildren(List<Resource> resources)
        {
            var ids = resources.Select(r => r.Id);
            var children = Set.Where(r => r.ParentId.HasValue && ids.Contains(r.ParentId.Value)).ToList();
            resources.ForEach(r => r.Children = children.Where(c => c.ParentId == r.Id).ToList());

            if(children.Count > 0)
            {
                LoadChildren(children);
            }
        }
    }
}
