using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public partial class Methods_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenericInjectionMethod()
        {
            // Act
            Container.RegisterType<OpenGenericInjectionMethod>(
                new InjectionMethod(nameof(OpenGenericInjectionMethod.InjectMe)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                new InjectionMethod(nameof(TypeWithMethodWithInvalidParameter.MethodWithRefParameter)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                new InjectionMethod(nameof(TypeWithMethodWithInvalidParameter.MethodWithOutParameter)));
        }

    }
}
