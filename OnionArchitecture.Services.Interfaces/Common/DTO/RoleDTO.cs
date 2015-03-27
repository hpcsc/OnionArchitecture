using System;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class RoleDTO : IEquatable<RoleDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(RoleDTO other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as RoleDTO;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
