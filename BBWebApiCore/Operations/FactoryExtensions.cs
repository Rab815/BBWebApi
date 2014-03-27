using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BloombergWebAPICore.Operations
{
    /// <summary>
    /// Specifically designed to create instances of other objects based on a type and variable number of arguments
    /// </summary>
    public static class FactoryExtensions
    {
        public static T New<T>(this Type type, params object[] args)
        {
            Type[] argTypes = args.Select(a => a.GetType()).ToArray();

            var ctor = type.GetConstructor(argTypes);

            return GetActivator<T>(ctor)(args);
        }

        public delegate T ObjectActivator<out T>(params object[] args);

        // precompiled constructor cache
        private static Dictionary<ConstructorInfo, object> _activators = new Dictionary<ConstructorInfo, object>();

        public static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            object activator;
            if (_activators.TryGetValue(ctor, out activator)) return (ObjectActivator<T>)activator;

            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

            var argsExp = new Expression[paramsInfo.Length];

            //pick each arg from the params array
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramsInfo[i].ParameterType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the NewExpression as body and our param object[] as arg
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            var compiled = (ObjectActivator<T>)lambda.Compile();

            _activators.Add(ctor, compiled);

            return compiled;
        }
    }


}
