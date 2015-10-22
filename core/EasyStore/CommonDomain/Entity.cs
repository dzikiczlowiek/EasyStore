namespace EasyStore.CommonDomain
{
    using System;

    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public bool Archived { get; protected set; }

        public virtual void MarkAsArchived()
        {
            this.Archived = true;
        }
    }
}
