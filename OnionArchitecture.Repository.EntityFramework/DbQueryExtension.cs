using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace OnionArchitecture.Repository.EntityFramework
{
    /// <summary>
    /// Extend DbSet and DbQuery to eager load all navigation properties. Only first level navigation properties loaded.
    /// </summary>
    public static class DbQueryExtension
    {
        public static IQueryable<T> IncludeNavigationProperties<T>(this DbSet<T> dbSet)
                                                                                    where T : class
        {
            DbQuery<T> objectQuery = dbSet;
            return IncludeNavigationProperties<T>(objectQuery);
        }

        public static IQueryable<T> IncludeNavigationProperties<T>(this DbQuery<T> dbQuery)
                                                                                    where T : class
        {
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).
                Where(p => p.CanRead && p.CanWrite && p.MemberType == MemberTypes.Property && p.GetGetMethod().IsVirtual &&
                        (p.PropertyType.Name == "ICollection`1" || !p.PropertyType.Namespace.StartsWith("System")));

            return properties.Aggregate(dbQuery, (current, property) => current.Include(property.Name));
        }
    }
}
