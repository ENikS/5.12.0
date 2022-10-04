using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Registrations
{
    public partial class RegisterType
    {
        [TestMethod]
        public void RegistrationsToArray()
        {
            // Setup
            Container.RegisterType(typeof(object));

            // Act
            var registrations = Container.Registrations.ToArray();

            // Validate
            Assert.IsNotNull(registrations);
        }

        [TestMethod]
        public void Type()
        {
            // Setup
            Container.RegisterType(typeof(object));

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void Named()
        {
            // Setup
            Container.RegisterType(typeof(object), Name);

            // Act
            var registration = Container.Registrations
                                        .FirstOrDefault(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void WithLifetime()
        {
            // Setup
            Container.RegisterType(typeof(object), new ContainerControlledLifetimeManager());

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
            Container.RegisterType(typeof(object), Name, new ContainerControlledLifetimeManager());

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
            Container.RegisterType(typeof(IService), typeof(Service));

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedNamed()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), Name);

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedWithLifetime()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void MappedNamedWithLifetime()
        {
            // Setup
            Container.RegisterType(typeof(IService), typeof(Service), Name, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations
                                        .Where(r => r.RegisteredType == typeof(IService))
                                        .First();

            // Validate
            Assert.IsNotNull(registration);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
            Assert.AreEqual(registration.RegisteredType, typeof(IService));
            Assert.AreEqual(registration.MappedToType, typeof(Service));
        }

        [TestMethod]
        public void Constructor()
        {
            // Setup
            Container.RegisterType(typeof(TypeWithAmbiguousCtors), new InjectionConstructor());

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, result.Signature);
        }

        [TestMethod]
        public void ConstructorWithData()
        {
            // Setup
            Container.RegisterType(typeof(TypeWithAmbiguousCtors), new InjectionConstructor("1", "2", "3"));

            // Act
            var result = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(TypeWithAmbiguousCtors.Four, result.Signature);
        }
    }
}
