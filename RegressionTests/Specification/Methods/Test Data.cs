using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods
    {
        #region Test Data

        public class InjectedTypeBase
        {
            private void PrivateMethod() { }

            protected void ProtectedMethod() { }

            public static void StaticMethod() { }
        }

        public class InjectedType : InjectedTypeBase
        {
            public void NormalMethod()
            {
            }

            public void OpenGenericMethod<T>()
            {
            }

            public void OutParamMethod<T>(out object arg)
            {
                arg = null;
            }

            public void RefParamMethod<T>(ref object arg)
            {
            }
        }

        public class AttributeOpenGenericType
        {
            [InjectionMethod]
            public void OpenGenericMethod<T>()
            {
            }
        }

        public class AttributeOutParamType
        {
            [InjectionMethod]
            public void OutParamMethod<T>(out object arg)
            {
                arg = null;
            }
        }

        public class AttributeRefParamType
        {
            [InjectionMethod]
            public void RefParamMethod<T>(ref object arg)
            {
            }
        }

        public class AttributeStaticType
        {
            [InjectionMethod]
            public static void Method() { }
        }

        public class AttributePrivateType
        {
            public bool Executed = false;

            [InjectionMethod]
            private void Method() { Executed = true; }
        }


        public class AttributeProtectedType
        {
            [InjectionMethod]
            protected void Method() { }
        }

        public class TypeWithMethodWithInvalidParameter
        {
            public void MethodWithRefParameter(ref string ignored)
            {
            }

            public void MethodWithOutParameter(out string ignored)
            {
                ignored = null;
            }
        }

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

        public interface IInjectedMethodTest
        {
            void Execute(string data);

            string Executed { get; }
        }

        public class InjectedMethodTest : IInjectedMethodTest
        {
            [InjectionMethod]
            public void ExecuteVoid() => ExecutedVoid = true;

            [InjectionMethod]
            public void Execute(string data)
            {
                Executed = data;
            }

            public bool ExecutedVoid { get; private set; }

            public string Executed { get; private set; }
        }

        public interface IGenericInjectedMethodTest<T>
        {
            void ExecuteGeneric(T data);

            T ExecutedGeneric { get; }
        }

        public class GenericInjectedMethodTest<T> : IGenericInjectedMethodTest<T>
        {
            public bool ExecutedVoid { get; private set; }

            [InjectionMethod]
            public void ExecuteVoid() => ExecutedVoid = true;


            [InjectionMethod]
            public void ExecuteGeneric(T data)
            {
                ExecutedGeneric = data;
            }

            public T ExecutedGeneric { get; private set; }
        }

        // Our generic interface 
        public interface ICommand<T>
        {
            T Executed { get; }

            ICommand<T> Chained { get; }

            void Execute(T data);

            void ChainedExecute(ICommand<T> inner);
        }

        // An implementation of ICommand that executes them.
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

        public interface I0 { }

        public interface I1 : I0 { }

        public interface I2 : I0 { }

        public class C1 : I1 { public C1(I2 i2) { } }

        public class C2 : I2 { public C2(I1 i1) { } }

        public class GuineaPig
        {
            public int IntValue;
            public string StringValue;
            public static bool StaticMethodWasCalled;

            public void Inject1()
            {
                StringValue = "void";
            }

            public void Inject2(string stringValue)
            {
                StringValue = stringValue;
            }

            public int Inject3(int intValue)
            {
                IntValue = intValue;
                return intValue * 2;
            }

            [InjectionMethod]
            public static void ShouldntBeCalled()
            {
                StaticMethodWasCalled = true;
            }
        }

        public class LegalInjectionMethod
        {
            public bool WasInjected;

            public void InjectMe()
            {
                WasInjected = true;
            }
        }

        public class OpenGenericInjectionMethod
        {
            public void InjectMe<T>()
            {
            }
        }

        public class OutParams
        {
            public void InjectMe(out int x)
            {
                x = 2;
            }
        }

        public class RefParams
        {
            public void InjectMe(ref int x)
            {
                x *= 2;
            }
        }

        public class InheritedClass : LegalInjectionMethod
        {
        }

        public class B1 : I1 { public B1(I1 i1) { } }

        public class F1 : I1
        {
            [InjectionMethod]
            public void Method(I1 i1) { }
        }

        public class G1 : I1 { public G1(I0 i0) { } }
        #endregion
    }
}
