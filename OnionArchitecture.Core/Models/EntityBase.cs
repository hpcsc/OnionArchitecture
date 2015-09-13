
using System;
namespace OnionArchitecture.Core.Models
{
    public class EntityBase<T> : IEquatable<EntityBase<T>> where T : IEquatable<T>
    {
        public T Id { get; set; }

        public bool Equals(EntityBase<T> other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
