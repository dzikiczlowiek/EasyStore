namespace EasyStore.Tests.Common
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Collections
    {
        public static ICollection<T> Join<T>(params IEnumerable<T>[] collections)
        {
            return collections.SelectMany(x => x).ToList();
        }
    }
}
