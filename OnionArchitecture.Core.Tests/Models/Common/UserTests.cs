using NUnit.Framework;
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;

namespace OnionArchitecture.Core.Tests.Models.Common
{
    public class UserTests
    {
        [TestFixture]
        public class HasAccessToMethod
        {
            private User FakeUser()
            {
                return new User
                {
                    Permissions = new List<Permission>
                    {
                        new Permission { ResourceId = 1, Type = PermissionType.Create },
                        new Permission { ResourceId = 2, Type = PermissionType.Deny },
                        new Permission { ResourceId = 3, Type = PermissionType.Read | PermissionType.Update },
                        new Permission { ResourceId = 5, Type = PermissionType.Read }
                    },
                    Roles = new List<Role>
                    {
                        new Role
                        {
                            Id = 1,
                            Permissions = new List<Permission>
                            {
                                new Permission { ResourceId = 1, Type = PermissionType.Deny },
                                new Permission { ResourceId = 2, Type = PermissionType.Create },
                                new Permission { ResourceId = 4, Type = PermissionType.Read },
                                new Permission { ResourceId = 5, Type = PermissionType.Read }
                            }  
                        }
                    }
                };
            }

            [Test]
            public void UserHasDenyPermission_ReturnFalse()
            {
                var resourceId = 2;
                var user = FakeUser();

                var result = user.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void OneOfUserRolesHasDenyPermission_ReturnFalse()
            {
                var resourceId = 1;
                var user = FakeUser();

                var result = user.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void NoPermissionForResourceAvailable_ReturnFalse()
            {
                var resourceId = 1;
                var user = FakeUser();
                user.Permissions.Clear();
                foreach (var role in user.Roles)
                {
                    role.Permissions.Clear();
                }

                var result = user.HasAccessTo(resourceId, PermissionType.Create);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void UserPermissionMatchesAndNoRolePermissionMatches_ReturnTrue()
            {
                var resourceId = 3;
                var user = FakeUser();

                var result = user.HasAccessTo(resourceId, PermissionType.Read);

                Assert.AreEqual(true, result);
            }

            [Test]
            public void UserPermissionNotMatchesAndOneOfRolePermissionMatches_ReturnTrue()
            {
                var resourceId = 4;
                var user = FakeUser();

                var result = user.HasAccessTo(resourceId, PermissionType.Read);

                Assert.AreEqual(true, result);
            }

            [Test]
            public void UserAndRolePermissionMatch_ReturnTrue()
            {
                var resourceId = 5;
                var user = FakeUser();

                var result = user.HasAccessTo(resourceId, PermissionType.Read);

                Assert.AreEqual(true, result);
            }
        }

        [TestFixture]
        public class HasDenyPermissionToMethod
        {
            private User FakeUser()
            {
                return new User
                {
                    Permissions = new List<Permission>
                    {
                        new Permission { ResourceId = 1, Type = PermissionType.Create },
                        new Permission { ResourceId = 2, Type = PermissionType.Deny },
                        new Permission { ResourceId = 3, Type = PermissionType.Deny }
                    },
                    Roles = new List<Role>
                    {
                        new Role
                        {
                            Id = 1,
                            Permissions = new List<Permission>
                            {
                                new Permission { ResourceId = 1, Type = PermissionType.Deny },
                                new Permission { ResourceId = 2, Type = PermissionType.Create },
                                new Permission { ResourceId = 3, Type = PermissionType.Deny }
                            }  
                        }
                    }
                };
            }

            [Test]
            public void HasNoPermission_ReturnFalse()
            {
                var resourceId = 1;
                var user = FakeUser();
                user.Permissions.Clear();
                foreach (var role in user.Roles)
                {
                    role.Permissions.Clear();
                }

                var result = user.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(false, result);
            }

            [Test]
            public void UserHasNoDenyPermissionAndRoleHasDenyPermission_ReturnTrue()
            {
                var resourceId = 1;
                var user = FakeUser();

                var result = user.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(true, result);
            }

            [Test]
            public void UserHasDenyPermissionAndRoleHasNoDenyPermission_ReturnTrue()
            {
                var resourceId = 2;
                var user = FakeUser();

                var result = user.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(true, result);
            }

            [Test]
            public void UserAndRoleHaveDenyPermission_ReturnTrue() 
            {
                var resourceId = 3;
                var user = FakeUser();

                var result = user.HasDenyPermissionTo(resourceId);

                Assert.AreEqual(true, result);
            }
        }
    }
}
