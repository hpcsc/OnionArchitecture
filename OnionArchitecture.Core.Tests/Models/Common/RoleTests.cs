using NUnit.Framework;
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Tests.Models.Common
{
    [TestFixture]
    public class RoleTests
    {
        [TestFixture]
        public class HasAccessToMethod
        {
            [Test]
            public void HasDenyPermission_ReturnFalse()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>
                    {
                        new Permission
                        {
                            ResourceId = resourceId,
                            Type = PermissionType.Deny
                        }
                    }
                };

                var result = role.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void NoPermissionForResourceAvailable_ReturnFalse()
            {
                var resourceId = 1;
                var role = new Role { Permissions = new List<Permission>() };

                var result = role.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void PermissionNotMatches_ReturnFalse()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>
                    {
                        new Permission
                        {
                            ResourceId = resourceId,
                            Type = PermissionType.Read | PermissionType.Update
                        }
                    }
                };

                var result = role.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void PermissionMatches_ReturnTrue()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>
                    {
                        new Permission
                        {
                            ResourceId = resourceId,
                            Type = PermissionType.Create
                        }
                    }
                };

                var result = role.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(true, result);
            }
        }

        [TestFixture]
        public class HasDenyPermissionToMethod
        {
            [Test]
            public void HasNoPermission_ReturnFalse()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>()
                };

                var result = role.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void HasNoDenyPermission_ReturnFalse()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>
                    {
                        new Permission
                        {
                            ResourceId = resourceId,
                            Type = PermissionType.Create
                        }
                    }
                };

                var result = role.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void HasDenyPermission_ReturnTrue()
            {
                var resourceId = 1;
                var role = new Role
                {
                    Permissions = new List<Permission>
                    {
                        new Permission
                        {
                            ResourceId = resourceId,
                            Type = PermissionType.Deny
                        }
                    }
                };

                var result = role.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(true, result);
            }
        }
    }
}
