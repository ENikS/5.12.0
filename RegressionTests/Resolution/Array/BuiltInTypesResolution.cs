using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Resolution
{
    public partial class Arrays
    {
        [TestMethod]
        public void ArrayOfRegistered()
        {
            // Arrange
            Container.RegisterInstance(new Service());
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<IService[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(2, Service.Instances);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void ArrayOfRegisteredPoco()
        {
            // Arrange
            Container.RegisterType<Service>("1");

            // Act
            var array = Container.Resolve<Service[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
        }

        [TestMethod]
        public void UnregisteredPoco()
        {
            // Act
            var array = Container.Resolve<Service[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void FuncNamed()
        {
            // Arrange
            Container.RegisterType(typeof(Func<>), "0");
            Container.RegisterType(typeof(Func<>), "1");
            Container.RegisterType(typeof(Func<>), "2");

            // Act
            var array = Container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.IsInstanceOfType(array, typeof(Func<IService>[]));
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void ClosedTrumpsOpenGeneric()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());

            Container.RegisterInstance<IFoo<IService>>(Name, instance)
                     .RegisterType(typeof(IFoo<>), typeof(Foo<>), Name)
                     .RegisterType<IFoo<IService>, Foo<IService>>("closed")
                     .RegisterType<IService, Service>();

            // Act
            var enumerable = Container.Resolve<IFoo<IService>[]>();

            // Assert
            Assert.AreEqual(2, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
        }
    }
}
