namespace EasyStore.DI
{
    using System;
    using System.Linq;
    using System.Reflection;

    using EasyStore;

    internal class ContainerRegistration : IContainerRegistration
    {
        private Func<EasyContainer, object> _resolve;

        private object _instance;

        private bool _instancePerCall;

        private Type _concreteType;

        private ConstructorInfo[] _constructors;

        public virtual IContainerRegistration Use<TConcrete>()
        {
            this._concreteType = typeof(TConcrete);
            this._constructors = this._concreteType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            return this;
        }

        public virtual IContainerRegistration Use(object instance)
        {
            if (Equals(instance, null))
            {
                throw new ArgumentNullException("instance", ExceptionMessages.InstanceCannotBeNull);
            }

            this._instance = instance;
            return this;
        }

        public virtual IContainerRegistration Use(Func<EasyContainer, object> resolve)
        {
            this._resolve = resolve;
            return this;
        }

        public virtual IContainerRegistration InstancePerCall()
        {
            this._instancePerCall = true;
            return this;
        }

        public virtual object Resolve(EasyContainer container)
        {
            if (this._concreteType != null)
            {
                var registeredType = container.RegisteredTypes();

                var cdata =
                    this._constructors.Select(x => new { Constructor = x, Parameters = x.GetParameters() })
                        .Where(c => c.Parameters.All(x => registeredType.Contains(x.ParameterType)))
                        .OrderByDescending(x => x.Parameters.Count())
                        .FirstOrDefault();

                if (cdata == null)
                {
                    throw new InvalidOperationException("Cant create instance");
                }
                var args = cdata.Parameters.Select(x => container.Resolve(x.ParameterType)).ToArray();

                var inst = cdata.Constructor.Invoke(args);

                return inst;
                //ParameterInfo[] paramsInfo = cdata.Parameters;

                //ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

                //Expression[] argsExp = new Expression[paramsInfo.Length];

                //for (int i = 0; i < paramsInfo.Length; i++)
                //{
                //    Expression index = Expression.Constant(i);
                //    Type paramType = paramsInfo[i].ParameterType;

                //    Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                //    Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                //    argsExp[i] = paramCastExp;
                //}

                //NewExpression newExp = Expression.New(cdata.Constructor, argsExp);

                //LambdaExpression lambda = Expression.Lambda(this._concreteType, newExp, param);


                //return lambda.Compile();
            }

            if (this._instancePerCall)
            {
                return this._resolve(container);
            }

            if (this._instance != null)
            {
                return this._instance;
            }

            return this._instance = this._resolve(container);
        }
    }
}
