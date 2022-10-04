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
#if !NET45
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectPrivateMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod("PrivateMethod"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectProtectedMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod("ProtectedMethod"));
        }
#endif

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectStaticMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.StaticMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectOpenGenericMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.OpenGenericMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectOutParamMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.OutParamMethod)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectRefParamMethod()
        {
            // Act
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.RefParamMethod)));
        }
    }
}
