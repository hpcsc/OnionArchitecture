
using System;
namespace OnionArchitecture.Core.Models.Common
{
    [Flags]
    public enum PermissionType
    {
        None = 0,
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8,
        Deny = 16
    }
}
