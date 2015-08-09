namespace EasyStore.CommonDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class EventRouter : IRouteEvents
    {
        private readonly IDictionary<Type, Action<object>> handlers = new Dictionary<Type, Action<object>>();

        private readonly bool throwOnApplyNotFound;

        public EventRouter()
            : this(true)
        {
        }

        public EventRouter(bool throwOnApplyNotFound)
        {
            this.throwOnApplyNotFound = throwOnApplyNotFound;
        }

        public EventRouter(bool throwOnApplyNotFound, IAggregate aggregate)
            : this(throwOnApplyNotFound)
        {
            this.Register(aggregate);
        }

        public virtual void Register<T>(Action<T> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.Register(typeof(T), @event => handler((T)@event));
        }

        public virtual void Register(IAggregate aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException("aggregate");
            }

            var applyMethods =
                aggregate.GetType()
                         .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         .Where(
                             m => m.Name == "Apply" && m.GetParameters().Length == 1 && m.ReturnParameter.ParameterType == typeof(void))
                         .Select(m => new { Method = m, MessageType = m.GetParameters().Single().ParameterType });

            foreach (var apply in applyMethods)
            {
                MethodInfo applyMethod = apply.Method;
                this.handlers.Add(apply.MessageType, m => applyMethod.Invoke(aggregate, new[] { m }));
            }
        }

        public virtual void Dispatch(object eventMessage)
        {
            if (eventMessage == null)
            {
                throw new ArgumentNullException("eventMessage");
            }

            Action<object> handler;
            if (this.handlers.TryGetValue(eventMessage.GetType(), out handler))
            {
                handler(eventMessage);
            }
            else if (this.throwOnApplyNotFound)
            {
                throw new InvalidOperationException("NOT FOUND AGGREGATE ROUTE");
            }
        }

        private void Register(Type messageType, Action<object> handler)
        {
            this.handlers[messageType] = handler;
        }
    }
}
