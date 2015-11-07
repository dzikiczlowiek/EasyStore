namespace EasyStore.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) 
            where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }

        public static bool PublicInstancePropertiesEqual(string typeName, object instance1, object instance2)
        {
            if (instance1 != null && instance2 != null)
            {
                var type = Type.GetType(typeName);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    let selfValue = type.GetProperty(pi.Name).GetValue(instance1, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(instance2, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }

            return instance1 == instance2;
        }
    }
}
