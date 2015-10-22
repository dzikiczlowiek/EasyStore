namespace EasyObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public interface ICollectionObjectBuilder<T> : IBuilder<ICollection<T>> 
    {
        ICollection<T> Build(int ammount);

        ICollectionObjectBuilder<T> All(Action<ISingleObjectBuilder<T>> createBuilder);

        ICollectionObjectBuilder<T> Next(int amount, Action<ISingleObjectBuilder<T>> createBuilder);
      
        ICollectionObjectBuilder<T> Next(Action<ISingleObjectBuilder<T>> createBuilder);

        ICollectionObjectBuilder<T> Next(int amount, T value);

        ICollectionObjectBuilder<T> Next(T value);

        void ClearChanges();
    }
}