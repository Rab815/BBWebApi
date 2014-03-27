using System;
using System.Collections.Generic;
using System.Reflection;
using Bloomberglp.Blpapi;
using BloombergWebAPICore.IWebApi;

namespace BloombergWebAPICore.Operations
{
    public class RequestProcessorFactory
    {
        private readonly Dictionary<string, Func<Session, IRequestContract, IRequestProcessor>> _factories = null;
        public RequestProcessorFactory()
        {
            // initialize with all base factory creators
            _factories = new Dictionary<string, Func<Session, IRequestContract,IRequestProcessor>>();

        }

        public void LoadFactories()
        {
            // easy to do is give a list of assemblies to look in to determine factories
            // though this doesn't exist yet, currently only looks in executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            var processorType = typeof(IRequestProcessor);
           
            foreach (var newtype in assembly.GetTypes())
            {

                if (!newtype.IsAbstract && !newtype.IsInterface && processorType.IsAssignableFrom(newtype))
                {
                   // Func<object, object> fastActivator = FastActivator.Generate(newtype, typeof(IRequestContract));
                   //_factories.Add(newtype.Name,(IRequestContract oContract) => (IRequestProcessor)fastActivator(oContract));

                    //_factories.Add(newtype.Name, (IRequestContract oContract) => (IRequestProcessor)new ObjectCreateMethod(newtype).CreateInstance(oContract));
                    //_factories.Add(newtype.Name, (IRequestContract oContract) => (IRequestProcessor) Activator.CreateInstance(newtype,oContract));
                    _factories.Add(newtype.Name,
                        (Session Session, IRequestContract oContract) => newtype.New<IRequestProcessor>(Session, oContract));

                }
            }

        }

        //public IRequestProcessor FactoryToRequestProcessor(IRequestContract oContract)
        //{
        //    //functional switch to determine which processor to create
        //    var type = oContract.GetType().Name.Replace("Contract", "Processor");
        //    var instance = _factories[type](oContract);
        //    return instance;
        //}

        public IRequestProcessor FactoryToRequestProcessor(Session s, IRequestContract oContract)
        {
            //functional switch to determine which processor to create
            var type = oContract.GetType().Name.Replace("Contract", "Processor");
            var instance = _factories[type](s, oContract);
            return instance;
        }

    }

    //public class ObjectCreateMethod
    //{
    //    delegate object MethodInvoker();
    //    MethodInvoker methodHandler = null;

    //    //delegate object MethodInvoker();
    //    //MethodInvoker methodHandler = null;

    //    public ObjectCreateMethod(Type type)
    //    {
    //        CreateMethod(type.GetConstructor(Type.EmptyTypes));
    //    }

    //    public ObjectCreateMethod(ConstructorInfo target)
    //    {
    //        CreateMethod(target);
    //    }

    //    void CreateMethod(ConstructorInfo target)
    //    {
    //        //Type[] methodArgs = { typeof(IRequestContract) };
    //        DynamicMethod dynamic = new DynamicMethod(string.Empty,
    //                    typeof(object),
    //                    new Type[0],
    //                    //methodArgs,
    //                    target.DeclaringType);
    //        ILGenerator il = dynamic.GetILGenerator();
    //        il.DeclareLocal(target.DeclaringType);
    //        il.Emit(OpCodes.Newobj, target);
    //        il.Emit(OpCodes.Stloc_0);
    //        il.Emit(OpCodes.Ldloc_0);
    //        il.Emit(OpCodes.Ret);

    //        methodHandler = (MethodInvoker)dynamic.CreateDelegate(typeof(MethodInvoker));
    //    }

    //    public object CreateInstance()
    //    {
    //        return methodHandler();
    //    }
    //}

    //public static class FastActivator
    //{
    //    public static Func<object, object> Generate(Type resultantType, Type parameterType)
    //    {
    //        ConstructorInfo constructorInfo = resultantType.GetConstructor(new Type[] { parameterType, });
    //        //#if DEBUG
    //        //ParameterInfo[] parameters = constructorInfo.GetParameters();
    //        //ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "boxed_" + parameters[0].Name);
    //        //#else
    //        ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
    //        //#endif
    //        Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(
    //            Expression.Block(
    //                Expression.IfThen(
    //                    Expression.Not(Expression.TypeIs(parameterExpression, parameterType)),
    //                    Expression.Throw(Expression.Constant(new ArgumentException("Parameter type mismatch.", parameterExpression.Name)))),
    //                Expression.Convert(
    //                    Expression.New(
    //                        constructorInfo,
    //                        Expression.Convert(parameterExpression, parameterType)),
    //                    resultantType)),
    //            parameterExpression);
    //        Func<object, object> functor = expression.Compile();
    //        return functor;
    //    }
    //}

}
