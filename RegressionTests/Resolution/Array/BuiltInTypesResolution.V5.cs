using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity;
using Unity.Injection;

namespace Resolution
{
    public partial class Arrays
    {
        [TestMethod]
        public void Array_Lazy_T()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Lazy<IService>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.AreEqual(2, Service.Instances);
        }

        [TestMethod]
        public void Array_Func_T()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();

            // Act
            var array = Container.Resolve<Func<IService>[]>();

            // Verify
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
        }

        [TestMethod]
        public void Array_Lazy_Func_T()
        {
            // Arrange
            Container.RegisterType(typeof(IList<>), typeof(List<>), new InjectionConstructor());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, Service>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Lazy<Func<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0].Value);
            Assert.IsNotNull(array[1].Value);
            Assert.IsNotNull(array[2].Value);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0].Value());
            Assert.IsNotNull(array[1].Value());
            Assert.IsNotNull(array[2].Value());
            Assert.AreEqual(3, Service.Instances);
        }

        [TestMethod]
        public void Array_Func_Lazy_T()
        {
            // Arrange
            Container.RegisterType<IService, Service>("1");
            Container.RegisterType<IService, Service>("2");
            Container.RegisterType<IService, OtherService>("3");
            Container.RegisterType<IService, Service>();
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Func<Lazy<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.IsNotNull(array[1]);
            Assert.IsNotNull(array[2]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.IsNotNull(array[1]().Value);
            Assert.IsNotNull(array[2]().Value);
            Assert.AreEqual(2, Service.Instances);
        }

        [TestMethod]
        public void Array_Func_ClosedGeneric()
        {
            // Arrange
            var instance = new Foo<IService>(new OtherService());
            Container.RegisterType<IService, Service>();

            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), "open");
            Container.RegisterType<IFoo<IService>, Foo<IService>>("closed");
            Container.RegisterInstance<IFoo<IService>>("Instance", instance);

            // Act
            var enumerable = Container.Resolve<Func<IFoo<IService>>[]>();

            // Assert
            Assert.AreEqual(3, enumerable.Length);
            Assert.IsNotNull(enumerable[0]);
            Assert.IsNotNull(enumerable[1]);
            Assert.IsNotNull(enumerable[2]);
        }

        [TestMethod]
        public void Array_Func_LazyInstance()
        {
            // Arrange
            Container.RegisterInstance(null, "Instance", new Lazy<IService>(() => new Service()));
            Service.Instances = 0;

            // Act
            var array = Container.Resolve<Func<Lazy<IService>>[]>();

            // Verify
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array);
            Assert.AreEqual(1, array.Length);
            Assert.IsNotNull(array[0]);
            Assert.AreEqual(0, Service.Instances);
            Assert.IsNotNull(array[0]().Value);
            Assert.AreEqual(1, Service.Instances);
        }
    }
}
