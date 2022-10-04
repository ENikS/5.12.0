using System;
#if V4
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Unity.Regression.Tests
{
    public class ValidatingResolverFactory
#if !NET45
        : IResolverFactory<Type>
#endif
    {
        private object _value;

        public ValidatingResolverFactory(object value)
        {
            _value = value;
        }

        public Type Type { get; private set; }
        public string Name { get; private set; }

#if !NET45
        public ResolveDelegate<TContext> GetResolver<TContext>(Type info)
            where TContext : IResolveContext
        {
            return (ref TContext context) =>
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            };
        }
#endif
    }
}
