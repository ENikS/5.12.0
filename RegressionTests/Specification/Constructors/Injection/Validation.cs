using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {
        [Ignore("v6, no validation during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoDefaultConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(new InjectionConstructor());
        }

        [Ignore("v6, no validation during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoBogusConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                new InjectionConstructor(typeof(int), typeof(string)));
        }

        [Ignore("v6, no validation during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoBogusValuesConstructor()
        {
            // Act
            Container.RegisterType<ClassWithTreeConstructors>(
                new InjectionConstructor( 1, "test"));
        }

        [TestMethod]
        public void Injection_SelectByValueTypes()
        {
            Container.RegisterType<TypeWithMultipleCtors>(new InjectionConstructor(new InjectionParameter(typeof(string)),
                new InjectionParameter(typeof(string)),
                new InjectionParameter(typeof(int))));
            Assert.AreEqual(TypeWithMultipleCtors.Three, Container.Resolve<TypeWithMultipleCtors>().Signature);
        }
    }
}
