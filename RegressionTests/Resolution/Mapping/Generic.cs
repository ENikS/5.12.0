using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Resolution
{
    public partial class Mapping
    {

        [TestMethod]
        public void GenericClosed()
        {
            // Arrange
            Container.RegisterType(typeof(Test<int>), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(ITest1<int>), typeof(Test<int>));
            Container.RegisterType(typeof(ITest2<int>), typeof(Test<int>));

            // Act
            var service1 = Container.Resolve<ITest1<int>>();
            var service2 = Container.Resolve<ITest2<int>>();

            // Assert
            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.AreSame(service1, service2);
        }

        [TestMethod]
        public void GenericOpen()
        {
            // Arrange
            Container.RegisterType(typeof(Test<>), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(ITest1<>), typeof(Test<>));
            Container.RegisterType(typeof(ITest2<>), typeof(Test<>));

            // Act
            var service1 = Container.Resolve<ITest1<int>>();
            var service2 = Container.Resolve<ITest2<int>>();

            // Assert
            Assert.IsNotNull(service1);
            Assert.IsNotNull(service2);

            Assert.AreSame(service1, service2);
        }

        [TestMethod]
        public void OpenGenericServicesCanBeResolved()
        {
            // Arrange
            Container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));

            // Act
            var genericService = Container.Resolve<IFoo<IService>>();
            var singletonService = Container.Resolve<IService>();

            // Assert
            Assert.AreSame(singletonService, genericService.Value);
        }

        [TestMethod]
        public void ClosedServicesPreferredOverOpenGenericServices()
        {
            // Arrange
            Container.RegisterType<IService, Service>();
            Container.RegisterType(typeof(IFoo<>), typeof(Foo<>));
            Container.RegisterType(typeof(IFoo<IService>), typeof(Foo<IService>));

            // Act
            var service = Container.Resolve<IFoo<IService>>();

            // Assert
            Assert.IsInstanceOfType(service.Value, typeof(Service));
        }
    }
}
