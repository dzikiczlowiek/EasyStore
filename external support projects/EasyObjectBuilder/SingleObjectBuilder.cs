namespace EasyObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SingleObjectBuilder<T> : ISingleObjectBuilder<T>, IBuilder
    {
        public SingleObjectBuilder()
        {
            this.Changes = new Dictionary<MemberInfo, Action<T>>();
            this.SubSingleObjectBuilders = new Dictionary<MemberInfo, IBuilder>();
            this.CreateInstance = Activator.CreateInstance<T>;
        }

        protected Func<T> CreateInstance { get; set; }

        protected IDictionary<MemberInfo, Action<T>> Changes { get; set; }

        protected IDictionary<MemberInfo, IBuilder> SubSingleObjectBuilders { get; set; }

        public T Build()
        {
            T item = this.CreateInstance();
            return this.Change(item);
        }

        object IBuilder.Build()
        {
            return this.Build();
        }

        public ISingleObjectBuilder<T> InitializeWith(Func<T> create)
        {
            this.CreateInstance = create;
            return this;
        }

        public ISingleObjectBuilder<T> With<TValue>(Expression<Func<T, TValue>> property, TValue setWith)
        {
            this.AddChange(property, setWith);
            return this;
        }

        public ISingleObjectBuilder<T> ForceWith<TValue>(Expression<Func<T, TValue>> property, TValue setWith)
        {
            this.ForceAddChange(property, setWith);
            return this;
        }

        public ISingleObjectBuilder<T> WithProperty<TSub>(Expression<Func<T, TSub>> property, Action<ISingleObjectBuilder<TSub>> build)
        {
            var member = ((MemberExpression)property.Body).Member;
            SingleObjectBuilder<TSub> subBuilder;
            IBuilder tempBuilder;
            if (!this.SubSingleObjectBuilders.TryGetValue(member, out tempBuilder))
            {
                subBuilder = new SingleObjectBuilder<TSub>();
                this.SubSingleObjectBuilders.Add(member, subBuilder);
            }
            else
            {
                subBuilder = tempBuilder as SingleObjectBuilder<TSub>;
            }

            build(subBuilder);
            this.AddChange(property, subBuilder.Build());
            return this;
        }

        public ISingleObjectBuilder<T> WithCollection<TCollection, TSub>(
            Expression<Func<T, TCollection>> property,
            Action<ICollectionObjectBuilder<TSub>> build)
            where TCollection : IEnumerable<TSub>
        {
            var builderType = typeof(CollectionObjectBuilder<>).MakeGenericType(typeof(TSub));
            var subListBuilder = (CollectionObjectBuilder<TSub>)Activator.CreateInstance(builderType);
            build(subListBuilder);
            this.AddChange(property, (TCollection)subListBuilder.Build());
            return this;
        }

        public ISingleObjectBuilder<T> WithDictionary<TKey, TSub>(
           Expression<Func<T, Dictionary<TKey, TSub>>> property,
           Action<ICollectionObjectBuilder<KeyValuePair<TKey, TSub>>> build)
        {
            var builderType = typeof(CollectionObjectBuilder<>).MakeGenericType(typeof(KeyValuePair<TKey, TSub>));
            var subListBuilder = (CollectionObjectBuilder<KeyValuePair<TKey, TSub>>)Activator.CreateInstance(builderType);
            build(subListBuilder);
            this.AddChange(property, subListBuilder.Build().ToDictionary(x => x.Key, x => x.Value));
            return this;
        }

        public void ClearChanges()
        {
            this.Changes.Clear();
        }

        public ISingleObjectBuilder<T> ResetChanges(params Expression<Func<T, object>>[] propertiesToReset)
        {
            foreach (var property in propertiesToReset)
            {
                MemberInfo member;
                var body = property.Body as MemberExpression;
                if (body != null)
                {
                    member = body.Member;
                }
                else
                {
                    var op = ((UnaryExpression)property.Body).Operand;
                    member = ((MemberExpression)op).Member;
                }

                if (this.Changes.ContainsKey(member))
                {
                    this.Changes.Remove(member);
                }
            }

            return this;
        }

        public ISingleObjectBuilder<T> CreateCopy()
        {
            var copy = new SingleObjectBuilder<T>();
            foreach (var change in this.Changes)
            {
                copy.Changes.Add(change);
            }

            return copy;
        }

        internal T Change(T original)
        {
            foreach (var change in this.Changes.Values)
            {
                change(original);
            }

            return original;
        }

        protected void AddChange<TValue>(Expression<Func<T, TValue>> property, TValue setWith)
        {
            var member = ((MemberExpression)property.Body).Member;
            var setter = GetSetter(property);
            Action<T> change = x => setter(x, setWith);
            if (!this.Changes.ContainsKey(member))
            {
                this.Changes.Add(member, change);
            }
            else
            {
                this.Changes[member] = change;
            }
        }

        protected void ForceAddChange<TValue>(Expression<Func<T, TValue>> property, TValue setWith)
        {
            var member = ((MemberExpression)property.Body).Member;
            var setter = GetSetter(property, true);
            Action<T> change = x => setter(x, setWith);
            if (!this.Changes.ContainsKey(member))
            {
                this.Changes.Add(member, change);
            }
            else
            {
                this.Changes[member] = change;
            }
        }

        private static Action<T, U> GetSetter<U>(Expression<Func<T, U>> expression, bool nonPublic = false)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod(nonPublic);

            var parameterT = Expression.Parameter(typeof(T), "x");
            var parameterU = Expression.Parameter(typeof(U), "y");

            var newExpression =
                Expression.Lambda<Action<T, U>>(
                    Expression.Call(parameterT, setMethod, parameterU),
                    parameterT,
                    parameterU);

            return newExpression.Compile();
        }
    }
}