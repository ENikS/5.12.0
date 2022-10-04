using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Properties
    {
        #region Test Data

        public class ObjectWithHiddenProperties
        {
            private string PrivateProperty { get; set; }

            public string ReadonlyProperty { get; }
        }

        public class ObjectWithThreeProperties : ObjectWithHiddenProperties
        {
            [Dependency]
            public string Name { get; set; }

            public object Property { get; set; }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public class ObjectWithNamedDependency
        {
            [Dependency(Name)]
            public string Property { get; set; }

            [OptionalDependency]
            public IUnityContainer Container { get; set; }
        }

        public class ObjectWithFourProperties : ObjectWithThreeProperties
        {
            public object SubProperty { get; set; }

            public object ReadOnlyProperty { get; }
        }

        public class ObjectWithDependency
        {
            public ObjectWithDependency(ObjectWithThreeProperties obj)
            {
                Dependency = obj;
            }

            public ObjectWithThreeProperties Dependency { get; }

        }

        // Our generic interface 
        public interface ICommand<T>
        {
            void Execute(T data);
            void ChainedExecute(ICommand<T> inner);
        }

        // An implementation of ICommand that executes them.
        public class ConcreteCommand<T> : ICommand<T>
        {
            private object p = null;

            public void Execute(T data)
            {
            }

            public void ChainedExecute(ICommand<T> inner)
            {
            }

            public object NonGenericProperty
            {
                get { return p; }
                set { p = value; }
            }
        }

        // And a decorator implementation that wraps an Inner ICommand<>
        public class LoggingCommand<T> : ICommand<T>
        {
            private ICommand<T> inner;

            public bool ChainedExecuteWasCalled = false;
            public bool WasInjected = false;

            public LoggingCommand(ICommand<T> inner)
            {
                this.inner = inner;
            }

            public LoggingCommand()
            {
            }

            public ICommand<T> Inner
            {
                get { return inner; }
                set { inner = value; }
            }

            public void Execute(T data)
            {
                // do logging here
                Inner.Execute(data);
            }

            public void ChainedExecute(ICommand<T> innerCommand)
            {
                ChainedExecuteWasCalled = true;
            }

            public void InjectMe()
            {
                WasInjected = true;
            }
        }


        // A couple of sample objects we're stuffing into our commands
        public class User
        {
            public void DoSomething(string message)
            {
            }
        }

        public class Account
        {
        }

        // InjectionParameterValue type used for testing nesting
        public struct Customer
        {
        }

#pragma warning disable 649
        public class DependencyAttributePrivateType
        {
            [Dependency]
            private object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 8;
        }
#pragma warning restore 649

        public class DependencyAttributeProtectedType
        {
            [Dependency]
            protected object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 9;
        }

        public class ObjectWithNamedDependencyProperties
        {
            [Dependency(Name)]
            public string Property { get; set; }

            public IUnityContainer Container { get; set; }
        }

        public class SimpleTestObject
        {
            public SimpleTestObject()
            {
            }

            [InjectionConstructor]
            public SimpleTestObject(int x)
            {
                X = x;
            }

            public int X { get; private set; }
        }

        public class ObjectThatDependsOnSimpleObject
        {
            public SimpleTestObject TestObject { get; set; }

            public ObjectThatDependsOnSimpleObject(SimpleTestObject testObject)
            {
                TestObject = testObject;
            }

            public SimpleTestObject OtherTestObject { get; set; }
        }

        public interface ISomething { }
        public class Something1 : ISomething { }
        public class Something2 : ISomething { }

        public class ObjectWithProperty
        {
            public ISomething MyProperty { get; set; }

            public ObjectWithProperty()
            {
            }

            public ObjectWithProperty(ISomething property)
            {
                MyProperty = property;
            }
        }

        public class Outer
        {
            public Outer(Inner inner, int logLevel)
            {
                this.Inner = inner;
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
            public Inner Inner { get; private set; }
        }

        public class Inner
        {
            public Inner(int logLevel, string label)
            {
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
        }

        #endregion
    }

    public partial class Properties_Diagnostic : Properties
    {
        #region Test Data

        public class DependencyInjectedTypeBase
        {
            private object PrivateProperty { get; set; }

            protected object ProtectedProperty { get; set; }

            public object ReadonlyProperty { get; }

            public static object StaticProperty { get; set; }

            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class DependencyInjectedType : DependencyInjectedTypeBase
        {
            public object NormalProperty { get; set; }
        }

        public class DependencyAttributeStaticType
        {
            [Dependency]
            public static object Dependency { get; set; }
        }

        public class OptionalDependencyAttributeStaticType
        {
            [OptionalDependency]
            public static object Dependency { get; set; }
        }

        public class DependencyAttributeIndexType
        {
            [Dependency]
            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class OptionalDependencyAttributeIndexType
        {
            [OptionalDependency]
            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class DependencyAttributeReadOnlyType
        {
            [Dependency]
            public object Dependency { get; }
        }

        public class OptionalDependencyAttributeReadOnlyType
        {
            [OptionalDependency]
            public object Dependency { get; }
        }

#pragma warning disable 649

        public class OptionalDependencyAttributePrivateType
        {
            [OptionalDependency]
            private object Dependency { get; set; }
        }

#pragma warning restore 649

        public class OptionalDependencyAttributeProtectedType
        {
            [OptionalDependency]
            protected object Dependency { get; set; }
        }

        public interface I0 { }

        public interface I1 : I0 { }
        public interface I2 : I0 { }

        public class B1 : I1 { public B1(I1 i1) { } }

        public class C1 : I1 { public C1(I2 i2) { } }

        public class C2 : I2 { public C2(I1 i1) { } }

        public class D1 : I1
        {
#if !NET45
            [Dependency]
#endif
            public I1 Field;
        }

        public class E1 : I1
        {
            [Dependency]
            public I1 Property { get; set; }
        }

        public class F1 : I1
        {
            [InjectionMethod]
            public void Method(I1 i1) { }
        }

        public class G0 : I0 { }
        public class G1 : I1 { public G1(I0 i0) { } }
        #endregion
    }
}
