namespace EasyObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CollectionObjectBuilder<T> : ICollectionObjectBuilder<T>
    {
        private readonly IList<BuildActionItem> buildActions = new List<BuildActionItem>();
       
        private int count;

        private BuildActionItem allObjectsBuildAction;

        public ICollection<T> Build()
        {
            return this.Build(this.count);
        }

        public ICollection<T> Build(int ammount)
        {
            var builders = new List<SingleObjectBuilder<T>>();
            foreach (var buildActionItem in this.buildActions)
            {
                builders.AddRange(Enumerable.Range(0, buildActionItem.Amount).Select(x => buildActionItem.Builder));
            }

            var list = new List<T>(ammount);
            for (int i = 0; i < ammount; i++)
            {
                var item = builders[i].Build();
                if (this.allObjectsBuildAction != null)
                {
                    this.allObjectsBuildAction.Builder.Change(item);
                }

                list.Add(item);
            }

            return list;
        }

        public ICollectionObjectBuilder<T> All(Action<ISingleObjectBuilder<T>> createBuilder)
        {
            var builder = new SingleObjectBuilder<T>();
            createBuilder(builder);
            this.allObjectsBuildAction = new BuildActionItem
            {
                Builder = builder
            };
            return this;
        }

        public ICollectionObjectBuilder<T> Next(int amount, Action<ISingleObjectBuilder<T>> createBuilder)
        {
            this.count += amount;
            var builder = new SingleObjectBuilder<T>();
            createBuilder(builder);
            this.buildActions.Add(new BuildActionItem
            {
                Amount = amount,
                Builder = builder
            });
            return this;
        }

        public ICollectionObjectBuilder<T> Next(T value)
        {
            return this.Next(1, value);
        }

        public ICollectionObjectBuilder<T> Next(int amount, T value)
        {
            Action<ISingleObjectBuilder<T>> createBuilder = b => b.InitializeWith(() => value);
            var builder = new SingleObjectBuilder<T>();
            createBuilder(builder);
            return this.Next(1, createBuilder);
        }

        public ICollectionObjectBuilder<T> Next(Action<ISingleObjectBuilder<T>> createBuilder)
        {
            return this.Next(1, createBuilder);
        }

        public void ClearChanges()
        {
            this.buildActions.Clear();
            this.allObjectsBuildAction = null;
        }

        private class BuildActionItem
        {
            public int Amount { get; set; }

            public SingleObjectBuilder<T> Builder { get; set; }
        }
    }
}
