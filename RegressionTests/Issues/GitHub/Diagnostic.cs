using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Issues
{
    public partial class GitHub_Diagnostic
    {
        [Ignore] // TODO: Fix
        [TestMethod]
        // https://github.com/unitycontainer/container/issues/160
        public void Issue_Container_160()
        {
            for (var i = 0; i < 10000; i++)
            {
                Container
                    .RegisterType<IFoo, Foo>()
                    .RegisterType<IBar, Bar>()
                    // It's important the name is random
                    .RegisterType<IBar, Bar>(Guid.NewGuid().ToString());

                var child = Container
                    .CreateChildContainer()
                    .RegisterType<IFoo, Foo>(new ContainerControlledLifetimeManager());

                var registrations = child.Registrations
                    .Where(r => r.RegisteredType == typeof(IFoo))
                    .ToList();

                Assert.IsNotNull(
                    registrations.FirstOrDefault(r => r.LifetimeManager is ContainerControlledLifetimeManager),
                    "Singleton registration not found on iteration #" + i);

                // This check fails on random iteration, usually i < 300.
                // It passes for v.5.8.13 but fails for v.5.9.0 and later both for .NET Core and for Framework.
                var registration = registrations.FirstOrDefault(r => r.LifetimeManager is TransientLifetimeManager);
                Assert.IsNull(registration, "Transient registration found on iteration #" + i);
            }
        }

#if !NET45
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/122
        public void Issue_Container_122()
        {
            Container.RegisterType<I1, C1>();
            Container.RegisterType<I2, C2>();

            //next line returns StackOverflowException
            Container.Resolve<I2>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_Container_149()
        {
            Container.RegisterInstance("123");
            var instance = Container.Resolve<Test_Class>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.AreEqual("123", instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        // https://github.com/unitycontainer/container/issues/149
        public void Issue_Container_149_Negative()
        {
            // BUG: StackOverflow happens here since 5.9.0
            var instance = Container.Resolve<Test_Class>(new DependencyOverride<string>(new OptionalParameter<string>()));
            Assert.IsNull(instance.Field);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // https://github.com/unitycontainer/container/issues/119
        public void Issue_Container_119()
        {
            // Setup
            Container.RegisterType<IInterface, Class1>();
            Container.RegisterType<IInterface, Class1>(nameof(Class1));
            Container.RegisterType<IInterface, Class2>(nameof(Class2));
            Container.RegisterType<IEnumerable<IInterface>, IInterface[]>();
            Container.RegisterType<ATestClass>();

            // Act
            var instance = Container.Resolve<ATestClass>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Value.Count());
        }
#endif
    }
}
