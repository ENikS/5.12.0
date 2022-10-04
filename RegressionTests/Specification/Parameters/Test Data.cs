using System;
#if V4
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        #region Test Data

        public class GenericService<T1, T2, T3>
        {
            public object Value { get; private set; }

            public int Called { get; private set; }

            public void Method(T1 value)
            {
                Value = value;
                Called = 1;
            }

            public void Method(T2 value)
            {
                Value = value;
                Called = 2;
            }

            public void Method(T3 value)
            {
                Value = value;
                Called = 3;
            }
        }

        public interface ICommand<T>
        {
            T Executed { get; }

            ICommand<T> Chained { get; }

            void Execute(T data);

            void ChainedExecute(ICommand<T> inner);
        }

        public class ConcreteCommand<T> : ICommand<T>
        {
            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }

            public void Execute(T data)
            {
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> inner)
            {
                Chained = inner;
            }
        }

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

            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }


            public ICommand<T> Inner
            {
                get { return inner; }
                set { inner = value; }
            }

            public void Execute(T data)
            {
                // do logging here
                Inner.Execute(data);
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> innerCommand)
            {
                ChainedExecuteWasCalled = true;
                Chained = innerCommand;
            }

            public void InjectMe()
            {
                WasInjected = true;
            }
        }

        public class Account
        {
        }

        public class InjectedType
        {
            #region Properties

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                Value = value;
            }

            public void MethodOne(object value)
            {
                ValueOne = value;
            }

            public void MethodOne(string value)
            {
                ValueTwo = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }
        }

        public class Service
        {
            public const string DefaultString = "default";
            public const int DefaultInt = 111;

            #region Properties

            public int Called { get; private set; } = -1;

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                ValueOne = value;
            }

            public void MethodOne(string value)
            {
                ValueOne = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }

            public void NoParameters() => Called = 0;

            public void NoAttributeParameter(object value)
            {
                Value = value;
                Called = 1;
            }

            public void DependencyAttribute([Dependency] object value)
            {
                Value = value;
                Called = 2;
            }

            public void NamedDependencyAttribute([Dependency(Name)] string value)
            {
                Value = value;
                Called = 3;
            }

            public void OptionalDependencyAttribute([OptionalDependency] object value)
            {
                Value = value;
                Called = 4;
            }

            public void OptionalNamedDependencyAttribute([OptionalDependency(Name)] string value)
            {
                Value = value;
                Called = 5;
            }

            public void OptionalDependencyAttributeMissing([OptionalDependency] IDisposable value)
            {
                Value = value;
                Called = 6;
            }

            public void OptionalNamedDependencyAttributeMissing([OptionalDependency(Name)] IDisposable value)
            {
                Value = value;
                Called = 7;
            }

            public void NoAttributeWithDefault(string value = DefaultString)
            {
                Value = value;
                Called = 8;
            }

            public void NoAttributeWithDefaultInt(int value = DefaultInt)
            {
                Value = value;
                Called = 9;
            }

            public void DependencyAttributeWithDefaultInt([Dependency] int value = DefaultInt)
            {
                Value = value;
                Called = 10;
            }

            public void NamedDependencyAttributeWithDefaultInt([Dependency(Name)] int value = DefaultInt)
            {
                Value = value;
                Called = 11;
            }

            public void OptionalDependencyAttributeWithDefaultInt([OptionalDependency] int value = DefaultInt)
            {
                Value = value;
                Called = 12;
            }

            public void OptionalNamedDependencyAttributeWithDefaultInt([OptionalDependency(Name)] int value = DefaultInt)
            {
                Value = value;
                Called = 13;
            }

            public void NoAttributeWithDefaultUnresolved(long value = 100)
            {
                Value = value;
                Called = 14;
            }

            public void WithDefaultDisposableUnresolved(IDisposable value = null)
            {
                Value = value;
                Called = 15;
            }

            public void DependencyAttributeWithDefaultNullUnresolved([Dependency] IDisposable value = null)
            {
                Value = value;
                Called = 16;
            }
        }

        public interface IService { }

        public class Service1 : IService { }

        public class Service2 : IService { }

        public interface IFoo { }

        public class Foo : IFoo
        {
            public object Fred { get; }

            public object George { get; }

            public Foo([OptionalDependency("Fred")] IService x,
                       [OptionalDependency("George")] IService y)
            {
                Fred = x;
                George = y;
            }
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

        public class TestType
        {
            private readonly string _dependency;

            public TestType() { }

            public TestType(string dependency)
            {
                _dependency = dependency;
            }

            public string DependencyField;
            public string DependencyProperty { get; set; }

            public override string ToString() => _dependency;
        }

        public interface IController
        {
            string GetMessage();
        }

        public class TheController : IController
        {
            private readonly IMessageProvider _messageProvider;

            public TheController(IMessageProvider messageProvider)
            {
                _messageProvider = messageProvider;
            }

            public string GetMessage()
            {
                return _messageProvider.CalculateMessage();
            }
        }

        public interface IMessageProvider
        {
            string CalculateMessage();
        }

        public class DefaultMessageProvider : IMessageProvider
        {
            public string CalculateMessage()
            {
                return "Hello World";
            }
        }

        public class AlternativeMessageProvider : IMessageProvider
        {
            public string CalculateMessage()
            {
                return "Goodbye cruel world!";
            }
        }

        public interface I0 { }

        public interface I1 : I0 { }

        public interface I2 : I0 { }

        public class B1 : I1 { public B1(I1 i1) { } }

        public class C1 : I1 { public C1(I2 i2) { } }

        public class C2 : I2 { public C2(I1 i1) { } }

        public class G0 : I0 { }

        public class G1 : I1 { public G1(I0 i0) { } }

        public class ObjectWithOptionalConstructorParameter
        {
            private IService someInterface;

            public IService SomeInterface { get { return someInterface; } }

            public ObjectWithOptionalConstructorParameter([OptionalDependency] IService someInterface)
            {
                this.someInterface = someInterface;
            }
        }

        public class ObjectWithNamedOptionalConstructorParameter
            : ObjectWithOptionalConstructorParameter
        {
            public ObjectWithNamedOptionalConstructorParameter([OptionalDependency(Name)] IService someInterface)
                : base(someInterface)
            {
            }
        }

        #endregion
    }
}
