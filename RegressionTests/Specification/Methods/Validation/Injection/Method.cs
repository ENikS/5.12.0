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
    public partial class Methods
    {
        [TestMethod]
        public void Injection_InjectedMethodIsCalled()
        {
            // Setup
            Container.RegisterType<LegalInjectionMethod>(
                new InjectionMethod(nameof(LegalInjectionMethod.InjectMe)));

            // Act
            LegalInjectionMethod result = Container.Resolve<LegalInjectionMethod>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }

        [Ignore("v6, No diagnostic during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<OutParams>(new InjectionMethod(nameof(OutParams.InjectMe), 12));
        }

        [Ignore("v6, No diagnostic during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<RefParams>(
                    new InjectionMethod(nameof(RefParams.InjectMe), 15));
        }

        [TestMethod]
        public void Injection_InheritedMethod()
        {
            // Setup
            Container.RegisterType<InheritedClass>(
                new InjectionMethod(nameof(InheritedClass.InjectMe)));

            // Act
            var result = Container.Resolve<InheritedClass>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }


        [TestMethod]
        public virtual void Injection_MethodPassingVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                new InjectionMethod(nameof(GuineaPig.Inject1)));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("void", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_ReturningVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                new InjectionMethod(nameof(GuineaPig.Inject2), "Hello"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("Hello", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_ReturningInt()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                new InjectionMethod(nameof(GuineaPig.Inject3), 17));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(17, pig.IntValue);
        }

        [TestMethod]
        public virtual void Injection_MultipleMethods()
        {
            // Setup
            Container.RegisterType<GuineaPig>(
                    new InjectionMethod(nameof(GuineaPig.Inject3), 37),
                    new InjectionMethod(nameof(GuineaPig.Inject2), "Hi there"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(37, pig.IntValue);
            Assert.AreEqual("Hi there", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_StaticMethod()
        {
            // Act
            Container.Resolve<GuineaPig>();

            // Verify
            Assert.IsFalse(GuineaPig.StaticMethodWasCalled);
        }

        [Ignore("v6, No diagnostic during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_InjectingStaticMethod()
        {
            // Verify
            Container.RegisterType<GuineaPig>(
                new InjectionMethod(nameof(GuineaPig.ShouldntBeCalled)));
        }
    }
}
