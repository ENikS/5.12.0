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
    public partial class IUnityContainer_Extensions
    {
        #region RegisterSingleton overloads

        #region Generics overloads

        [TestMethod]
        public virtual void RegisterSingleton_T()
        {
            // Act
            Container.RegisterSingleton<Service>(new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_T_Name()
        {
            // Act
            Container.RegisterSingleton<Service>(Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_TFrom_TTo()
        {
            // Act
            Container.RegisterSingleton<IService, Service>(new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_TFrom_TTo_Name()
        {
            // Act
            Container.RegisterSingleton<IService, Service>(Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        #endregion

        #region Non-generics overloads

        [TestMethod]
        public virtual void RegisterSingleton_Type()
        {
            // Act
            Container.RegisterSingleton(typeof(Service), new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_Type_Name()
        {
            // Act
            Container.RegisterSingleton(typeof(Service), Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_Type_Type()
        {
            // Act
            Container.RegisterSingleton(typeof(IService), typeof(Service), new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public virtual void RegisterSingleton_Type_Type_Name()
        {
            // Act
            Container.RegisterSingleton(typeof(IService), typeof(Service), Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        #endregion

        #endregion
    }
}
