namespace EasyStore.CommonDomain
{
    using System;
    using System.Collections.Generic;

    public abstract class AggregateRoot : Entity, IAggregate
    {
        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

        private IRouteEvents _eventRouter;

        private IEventStream _stream;

        protected AggregateRoot(IRouteEvents eventRouter)
        {
            this.ResolveRouter(eventRouter);
        }

        protected AggregateRoot(Guid aggregateId)
            : this(null)
        {
            this.RaiseEvent(new CreatedEvent(aggregateId));
        }

        protected AggregateRoot(Guid aggregateId, IRouteEvents eventRouter)
        {
            this.ResolveRouter(eventRouter);
            this.RaiseEvent(new CreatedEvent(aggregateId));
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

        public virtual bool Equals(AggregateRoot other)
        {
            return null != other && other.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        protected virtual void Apply(CreatedEvent @event)
        {
            this.Id = @event.AggregateId;
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

        void IAggregate.AttachToStream(IEventStream stream)
        {
            this._stream = stream;
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            ((IAggregate)this).ApplyEvent(@event);
            this._uncommittedEvents.Add(@event);
            if (this._stream != null)
            {
                this._stream.ForwardEvent(this, @event);
            }
        }

        private void ResolveRouter(IRouteEvents eventRouter)
        {
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
    }
}
