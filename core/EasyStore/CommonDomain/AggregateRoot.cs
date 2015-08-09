namespace EasyStore.CommonDomain
{
    using System;
    using System.Collections.Generic;

    public abstract class AggregateRoot : Entity, IAggregate
    {
        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        private IRouteEvents _eventRouter;

        protected AggregateRoot(Guid aggregateId)
            : this(aggregateId, null)
        {
        }

        protected AggregateRoot(Guid aggregateId, IRouteEvents eventRouter)
        {
            this.Id = aggregateId;
            if (eventRouter == null)
            {
                this._eventRouter = new EventRouter(true, this);
            }
            else
            {
                this._eventRouter = eventRouter;
                this._eventRouter.Register(this);
            }
        }

        public int Version { get; private set; }

        protected IRouteEvents RegisteredRoutes
        {
            get
            {
                return this._eventRouter;
            }

            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("AggregateBase must have an event router to function");
                }

                this._eventRouter = value;
            }
        }

        public virtual bool Equals(IAggregate other)
        {
            return null != other && other.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IAggregate);
        }

        void IAggregate.ApplyEvent(IDomainEvent eventData)
        {
            this.RegisteredRoutes.Dispatch(eventData);
            this.Version++;
        }

        ICollection<IDomainEvent> IAggregate.GetUncommittedEvents()
        {
            return this._uncommittedEvents;
        }

        void IAggregate.ClearUncommittedEvents()
        {
            this._uncommittedEvents.Clear();
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            ((IAggregate)this).ApplyEvent(@event);
            this._uncommittedEvents.Add(@event);
        }
    }
}
