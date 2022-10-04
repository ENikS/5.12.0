using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Practices.Unity
{
    internal static class Compatibility_v_4
    {
        #region Register Factory

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, Type, string, object> factory) 
            => container.RegisterType(type, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, Type, string, object> factory) 
            => container.RegisterType(type, name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, name, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, object> factory) 
            => container.RegisterType(type, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, object> factory) 
            => container.RegisterType(type, name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, name, manager, new InjectionFactory(factory));



        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, Type, string, object> factory)
            => container.RegisterType(typeof(T), new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, Type, string, object> factory)
            => container.RegisterType(typeof(T), name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), name, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, object> factory)
            => container.RegisterType(typeof(T), new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, object> factory)
            => container.RegisterType(typeof(T), name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), name, manager, new InjectionFactory(factory));

        #endregion


        #region Singleton

        public static IUnityContainer RegisterSingleton<T>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, typeof(T), null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<T>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, typeof(T), name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(typeof(TFrom), typeof(TTo), null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<TFrom, TTo>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(typeof(TFrom), typeof(TTo), name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type type, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, type, null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type type, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, type, name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type from, Type to, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(from, to, null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type from, Type to, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(from, to, name, new ContainerControlledLifetimeManager(), injectionMembers);
        }
        
        #endregion
    }

    public static partial class Inject
    {
        #region Array

        public static ResolvedArrayParameter Array(Type elementType, params object[] elementValues)
            => new ResolvedArrayParameter(elementType, elementValues);

        public static ResolvedArrayParameter Array<TElement>(params object[] elementValues)
            => new ResolvedArrayParameter(typeof(TElement), elementValues);

        #endregion


        #region Parameter

        public static InjectionParameter Parameter(object value) => new InjectionParameter(value);

        public static InjectionParameter Parameter(Type type, object value)
            => new InjectionParameter(type ?? throw new ArgumentNullException(nameof(type)), value);

        public static InjectionParameter Parameter<TTarget>(object value) => new InjectionParameter(typeof(TTarget), value);

        #endregion


        //#region Field

        //public static InjectionMember Field(string name, object value)
        //    => new InjectionField(name ?? throw new ArgumentNullException(nameof(name)), value);

        //#endregion


        #region Property

        public static InjectionMember Property(string name, object value)
            => new InjectionProperty(name ?? throw new ArgumentNullException(nameof(name)), value);

        #endregion
    }

    public static partial class Override
    {
        public static ResolverOverride Property(string name, object value) => new PropertyOverride(name, value);


        #region Field

        //public static ResolverOverride Field(string name, object value) => new FieldOverride(name, value);

        #endregion


        #region Parameter

        //public static ResolverOverride Parameter(object value)
        //    => new ParameterOverride(value?.GetType() ?? throw new ArgumentNullException(nameof(value)), value);


        public static ResolverOverride Parameter(string name, object value)
            => new ParameterOverride(name, value);

        //public static ResolverOverride Parameter(Type type, object value)
        //    => new ParameterOverride(type, value);


        public static ResolverOverride Parameter(Type type, string name, object value)
            => new ParameterOverride(name, value).OnType(type);

        //public static ResolverOverride Parameter<TType>(object value)
        //    => new ParameterOverride(typeof(TType), value);

        public static ResolverOverride Parameter<TType>(string name, object value)
            => Parameter(typeof(TType), name, value);

        #endregion


        #region Dependency

        //public static ResolverOverride Dependency(object value)
        //    => Dependency(value?.GetType() ?? throw new ArgumentNullException(nameof(value)), null, value);

        //public static ResolverOverride Dependency(string name, object value)
        //    => Dependency(value?.GetType() ?? throw new ArgumentNullException(nameof(value)), name, value);


        public static ResolverOverride Dependency(Type type, object value)
        {
            return new DependencyOverride(type, value);
        }

        //public static ResolverOverride Dependency(Type type, string? name, object value)
        //{
        //    return new DependencyOverride(type, name, value);
        //}


        public static ResolverOverride Dependency<TType>(object value)
            => new DependencyOverride(typeof(TType), value);

        //public static ResolverOverride Dependency<TType>(string? name, object value)
        //    => new DependencyOverride(typeof(TType), name, value);

        #endregion
    }

    public static partial class Resolve
    {
        #region Dependency

        public static ResolvedParameter Dependency<TTarget>() => new ResolvedParameter(typeof(TTarget));

        public static ResolvedParameter Dependency<TTarget>(string name) => new ResolvedParameter(typeof(TTarget), name);

        #endregion


        #region Parameter


        public static ResolvedParameter Parameter(Type type) => new ResolvedParameter(type);

        public static ResolvedParameter Parameter<TTarget>() => new ResolvedParameter(typeof(TTarget));

        public static ResolvedParameter Parameter(Type type, string name) => new ResolvedParameter(type, name);

        public static ResolvedParameter Parameter<TTarget>(string name) => new ResolvedParameter(typeof(TTarget), name);

        #endregion


        #region Generic

        public static GenericParameter Generic(string genericParameterName) => new GenericParameter(genericParameterName);

        public static GenericParameter Generic(string genericParameterName, string registrationName) => new GenericParameter(genericParameterName, registrationName);

        #endregion


        #region Optional


        public static OptionalParameter Optional(Type type) => new OptionalParameter(type);

        public static OptionalParameter Optional<TTarget>() => new OptionalParameter(typeof(TTarget));

        public static OptionalParameter Optional(Type type, string name) => new OptionalParameter(type, name);

        public static OptionalParameter Optional<TTarget>(string name) => new OptionalParameter(typeof(TTarget), name);

        #endregion

        #region Property

        public static InjectionMember Property(string name) => new InjectionProperty(name ?? throw new ArgumentNullException(nameof(name)));

        public static InjectionMember OptionalProperty(string name) => new InjectionProperty(name ?? throw new ArgumentNullException(nameof(name)), true);

        #endregion
    }
}