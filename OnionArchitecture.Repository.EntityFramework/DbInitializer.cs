using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;
using System.Data.Entity;

namespace OnionArchitecture.Repository.EntityFramework
{
    public class DbInitializer : DropCreateDatabaseAlways<OnionArchDbContext>
    {
        protected override void Seed(OnionArchDbContext context)
        {
            var roles = new List<Role>
            {
                new Role {Name = "Admin"},
                new Role {Name = "Standard"},
                new Role {Name = "Super"}
            };

            roles.ForEach(r => context.Set<Role>().Add(r));

            context.SaveChanges();

            var users = new List<User>
            {
                new User
                {
                    UserName = "admin",
                    FullName = "Admin",
                    Password = "admin",
                    Status = UserStatus.Active,
                    Roles = new List<Role> {roles[0]}
                },
                new User
                {
                    UserName = "standard",
                    FullName = "Standard user",
                    Password = "standard",
                    Status = UserStatus.Active,
                    Roles = new List<Role> {roles[1]}
                },
                new User
                {
                    UserName = "super",
                    FullName = "Super user",
                    Password = "super",
                    Status = UserStatus.Active,
                    Roles = new List<Role> {roles[2]}
                }
            };

            users.ForEach(r => context.Set<User>().Add(r));

            context.SaveChanges();

            var resources = new List<Resource>
            {
                new Resource 
                { 
                    Name = "Menu", 
                    Children = new List<Resource>
                    {
                        new Resource { 
                            Name = "About",
                            Description = "Resource to control permission to view About menu",
                            Permissions = new List<Permission>
                            {
                                new Permission
                                {
                                    UserId = users[0].Id,
                                    Type  = PermissionType.Read | PermissionType.Update
                                }
                            }
                        },
                        new Resource 
                        { 
                            Name = "Contact",
                            Description = "Resource to control permission to view Contact menu",
                            Permissions = new List<Permission>
                            {
                                new Permission
                                {
                                    UserId = users[1].Id,
                                    Type  = PermissionType.Read | PermissionType.Create
                                }
                            }
                        },             
                        new Resource 
                        { 
                            Name = "Permission",
                            Description = "Resource to control permission to view Permission menu",
                            Permissions = new List<Permission>
                            {
                                new Permission
                                {
                                    UserId = users[2].Id,
                                    Type  = PermissionType.Read | PermissionType.Delete | PermissionType.Deny
                                }
                            }
                        }
                    }
                }
            };

            resources.ForEach(r => context.Set<Resource>().Add(r));

            context.SaveChanges();
        }
    }
}
