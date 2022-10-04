using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Resolution
{
    public partial class Generics
    {
        [TestMethod]
        public void GenericService()
        {
            // Arrange
            Container.RegisterType(typeof(Foo<>));

            // Act 
            var instance = Container.Resolve<Foo<Service>>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(Foo<Service>));
        }

        [TestMethod]
        public void GenericIService()
        {
            // Arrange
            Container.RegisterType(typeof(IService), typeof(Service))
                     .RegisterType(typeof(Foo<>));

            // Act 
            var instance = Container.Resolve<Foo<IService>>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(Foo<IService>));
        }

        [TestMethod]
        public void GenericMapping()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));

            // Act 
            var instance = Container.Resolve<IFoo<IService>>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IFoo<IService>));
        }

        [TestMethod]
        public void Named_null_null()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IFoo<IService>));
        }

        [TestMethod]
#if V4
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
#else
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public void Named_null_Name_null()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            var instance = Container.Resolve<IFoo<IOtherService>>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IFoo<IOtherService>));
        }

        [TestMethod]
#if V4
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
#else
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public void Named_null_Name_name()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IFoo<IService>));
        }

        [TestMethod]
        public void Named_Name_Name_Name()
        {
            // Arrange
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>), Name);
            Container.RegisterType<IOtherService, OtherService>(Name);

            // Act 
            var instance = Container.Resolve<IFoo<IService>>(Name);

            // Validate
            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(IFoo<IService>));
        }
    }
}
