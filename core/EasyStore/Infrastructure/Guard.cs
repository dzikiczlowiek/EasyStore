namespace EasyStore.Infrastructure
{
    using System;
    using System.Linq.Expressions;

    public static class Guard
    {
        public static void NotNull<T>(Expression<Func<T>> property)
        {
            var func = property.Compile();
            if (func() == null)
            {
                var propertyName = GetParameterName(property);
                throw new ArgumentNullException(propertyName);
            }
        }

        private static string GetParameterName(LambdaExpression reference)
        {
            return ((MemberExpression)reference.Body).Member.Name;
        }
    }
}
