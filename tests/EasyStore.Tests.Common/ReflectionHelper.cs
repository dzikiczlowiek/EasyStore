namespace EasyStore.Tests.Common
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionHelper
    {
        public static void SetPrivateProperty<T, TP>(this T _this, Expression<Func<T, TP>> expression, TP value)
        {
            var setExpression = GetSetter(expression, true);
            setExpression(_this, value);
        }

        public static TF GetPrivateFieldValue<T, TF>(this T _this, string fieldName)
        {
            var getExpression = GetFieldAccessor<T, TF>(fieldName);
            return getExpression(_this);
        }

        public static Func<T, TF> GetFieldAccessor<T, TF>(string fieldName)
        {
            var param = Expression.Parameter(typeof(T), "arg");
            var member = Expression.Field(param, fieldName);
            var lambda = Expression.Lambda(typeof(Func<T, TF>), member, param);
            
            Func<T, TF> compiled = (Func<T, TF>)lambda.Compile();
            return compiled;
        }

        public static Action<T, TP> GetSetter<T, TP>(Expression<Func<T, TP>> expression, bool nonPublic = false)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod(nonPublic);

            var parameterT = Expression.Parameter(typeof(T), "x");
            var parameterU = Expression.Parameter(typeof(TP), "y");

            var newExpression = Expression.Lambda<Action<T, TP>>(
                Expression.Call(parameterT, setMethod, parameterU),
                parameterT,
                parameterU);

            return newExpression.Compile();
        }
    }
}
