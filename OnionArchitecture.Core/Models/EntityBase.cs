
using System;
namespace OnionArchitecture.Core.Models
{
    public class EntityBase : IEquatable<EntityBase>
    {
        public int Id { get; set; }

        public bool Equals(EntityBase other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            EntityBase other = obj as EntityBase;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
