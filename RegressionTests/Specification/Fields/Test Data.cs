using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Fields
    {
        #region Test Data

        public class ValidatingResolverFactory : IResolverFactory<Type>
        {
            private object _value;

            public ValidatingResolverFactory(object value)
            {
                _value = value;
            }

            public Type Type { get; private set; }
            public string Name { get; private set; }

            public ResolveDelegate<TContext> GetResolver<TContext>(Type type)
                where TContext : IResolveContext
            {
                return (ref TContext context) =>
                {
                    Type = type;
                    Name = context.Name;

                    return _value;
                };
            }
        }

        public class ValidatingResolver : IResolve
        {
            private object _value;

            public ValidatingResolver(object value)
            {
                _value = value;
            }

            public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            }

            public Type Type { get; private set; }

            public string Name { get; private set; }
        }

        public class ObjectWithThreeFields
        {
            [Dependency]
            public string Name;

            public object Field;

            [Dependency]
            public IUnityContainer Container;
        }

        public class ObjectWithNamedDependency
        {
            [Dependency(Name)]
            public string Field;

            [Dependency]
            public IUnityContainer Container;
        }

        public interface IFoo { }
        public class Foo : IFoo { }

        public class ObjectWithOptionalFields
        {
            [Dependency]
            public string Name;

            public IFoo Field;

            [Dependency]
            public IUnityContainer Container;
        }


        public class ObjectWithAttributes
        {
            [Dependency("name1")]
            public string Dependency;

            [OptionalDependency("other")]
            public string Optional;
        }

        public class ObjectWithFourFields : ObjectWithThreeFields
        {
            public object SubField;

            public readonly object ReadOnlyField;
        }

        public class ObjectWithDependency
        {
            public ObjectWithDependency(ObjectWithThreeFields obj)
            {
                Dependency = obj;
            }

            public ObjectWithThreeFields Dependency { get; }

        }

#pragma warning disable 649
        public class DependencyAttributePrivateType
        {
            [Dependency]
            private object Dependency;

            public object Value => Dependency;
            public int Called = 8;
        }
#pragma warning restore 649

        public class DependencyAttributeProtectedType
        {
            [Dependency] protected object Dependency;

            public object Value => Dependency;
            public int Called = 9;
        }


        public interface I0 { }

        public interface I1 : I0 { }
        public interface I2 : I0 { }

        public class B1 : I1 { public B1(I1 i1) { } }

        public class C1 : I1 { public C1(I2 i2) { } }

        public class C2 : I2 { public C2(I1 i1) { } }

        public class D1 : I1
        {
            [Dependency]
            public I1 Field;
        }

        public class G0 : I0 { }
        public class G1 : I1 { public G1(I0 i0) { } }


        #endregion
    }
}
