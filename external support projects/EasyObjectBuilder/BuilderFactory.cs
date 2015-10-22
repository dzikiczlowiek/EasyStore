namespace EasyObjectBuilder
{
    public static class BuilderFactory
    {
        public static ISingleObjectBuilder<T> CreateSingle<T>()
        {
            return new SingleObjectBuilder<T>();
        }

        public static ICollectionObjectBuilder<T> CreateList<T>()
        {
            return new CollectionObjectBuilder<T>();
        }
    }
}