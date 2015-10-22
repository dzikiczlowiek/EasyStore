namespace EasyObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface ISingleObjectBuilder<T> : IBuilder<T>
    {
        ISingleObjectBuilder<T> InitializeWith(Func<T> create);

        ISingleObjectBuilder<T> With<TValue>(Expression<Func<T, TValue>> property, TValue setWith);

        ISingleObjectBuilder<T> ForceWith<TValue>(Expression<Func<T, TValue>> property, TValue setWith);

        ISingleObjectBuilder<T> WithProperty<TSub>(Expression<Func<T, TSub>> property, Action<ISingleObjectBuilder<TSub>> build);

        ISingleObjectBuilder<T> WithCollection<TCollection, TSub>(
            Expression<Func<T, TCollection>> property,
            Action<ICollectionObjectBuilder<TSub>> build)
            where TCollection : IEnumerable<TSub>;

        ISingleObjectBuilder<T> WithDictionary<TKey, TSub>(
            Expression<Func<T, Dictionary<TKey, TSub>>> property,
            Action<ICollectionObjectBuilder<KeyValuePair<TKey, TSub>>> build);

        void ClearChanges();

        ISingleObjectBuilder<T> ResetChanges(params Expression<Func<T, object>>[] propertiesToReset);

        ISingleObjectBuilder<T> CreateCopy();
    }
}