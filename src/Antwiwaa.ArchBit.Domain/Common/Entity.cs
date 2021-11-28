using System;

namespace Antwiwaa.ArchBit.Domain.Common
{
    public abstract class Entity<TId> : AuditableEntity
    {
        protected Entity(TId id)
        {
            if (Equals(id, default(TId)))
                throw new ArgumentException("The ID cannot be the type's default value.", nameof(id));

            Id = id;
        }

        // EF requires an empty constructor
        protected Entity()
        {
        }

        public TId Id { get; protected set; }
    }
}