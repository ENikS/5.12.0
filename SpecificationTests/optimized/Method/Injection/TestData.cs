using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Specification.Method.Injection
{
    public abstract partial class SpecificationTests : TestFixtureBase
    {
        #region Test Data

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
            public bool Test0;
            public bool Test1;
            public bool Test2;

            public void InjectMe()
            {
                WasInjected = true;
            }

            public void TestMethod()
            {
                Test0 = true;
            }

            public void TestMethod(string data)
            {
                Test1 = true;
            }

            public void TestMethod(long data)
            {
                Test2 = true;
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

        #endregion
    }
}
