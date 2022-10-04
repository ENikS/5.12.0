using System;
#if V4
using Microsoft.Practices.ObjectBuilder2;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Unity.Regression.Tests
{
    public class ValidatingResolver
#if V4
            : IDependencyResolverPolicy
#else
            : IResolve
#endif

    {
        private object _value;

        public ValidatingResolver(object value)
        {
            _value = value;
        }

#if V4

        public object Resolve(IBuilderContext context)
        {
            Type = context.BuildKey.Type;
            Name = context.BuildKey.Name;

            return _value;
        }
#else
            public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            }
#endif

        public Type Type { get; private set; }

        public string Name { get; private set; }
    }
}
