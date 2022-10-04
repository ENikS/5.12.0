using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Public.API
{
    public partial class IUnityContainer_Registrations
    {
        [TestMethod]
        public void RegisteredType()
        {
            // Setup
            Container.RegisterType<object>();

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
#if !NET45
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
#endif
        }

        [TestMethod]
        public void RegisteredNamed()
        {
            // Setup
            Container.RegisterType<object>(Name);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
#if !NET45
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
#endif
        }

        [TestMethod]
        public void WithLifetime()
        {
            // Setup
            Container.RegisterType<object>(new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void NamedWithLifetime()
        {
            // Setup
            Container.RegisterType<object>(Name, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }


        [TestMethod]
        public void MappedType()
        {
            // Setup
            Container.RegisterType<IService, Service>();

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => null == r.Name && typeof(IService) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
#if !NET45
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
#endif
        }

        [TestMethod]
        public void MappedNamed()
        {
            // Setup
            Container.RegisterType<IService, Service>(Name);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r =>Name == r.Name && typeof(IService) == r.RegisteredType);
            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
#if !NET45
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
#endif
        }

        [TestMethod]
        public void MappedWithLifetime()
        {
            // Setup
            Container.RegisterType<IService, Service>(new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => null == r.Name && typeof(IService) == r.RegisteredType);
            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
#if !NET45
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
#endif
        }

        [TestMethod]
        public void MappedNamedWithLifetime()
        {
            // Setup
            Container.RegisterType<IService, Service>(Name, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => Name == r.Name && typeof(IService) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }
    }
}
