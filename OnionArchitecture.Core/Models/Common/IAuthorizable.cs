
namespace OnionArchitecture.Core.Models.Common
{
    public interface IAuthorizable
    {
        bool HasAccessTo(int resourceId, PermissionType type);
        bool HasDenyPermissionTo(int resourceId);
    }
}
