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
        [TestMethod]
        public void RegisterType()
        {
            // Arrange
            var typeFrom = typeof(IService);
            var typeTo = typeof(Service);
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType(typeFrom, typeTo, Name, manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeFrom, registration.RegisteredType);
            Assert.AreEqual(typeTo, registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #region RegisterType overloads

        #region Generics overloads

        [TestMethod]
        public virtual void RegisterType_T()
        {
            // Act
            Container.RegisterType<Service>(new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegisterType_T_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType<Service>(manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_T_Name()
        {
            // Act
            Container.RegisterType<Service>(Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
        }

        [TestMethod]
        public void RegisterType_T_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType<Service>(Name, manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_TFrom_TTo()
        {
            // Act
            Container.RegisterType<IService, Service>(new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegisterType_TFrom_TTo_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType<IService, Service>(manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_TFrom_TTo_Name()
        {
            // Act
            Container.RegisterType<IService, Service>(Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
        }

        [TestMethod]
        public void RegisterType_TFrom_TTo_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType<IService, Service>(Name, manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion

        #region Non-generics overloads

        [TestMethod]
        public virtual void RegisterType_Type()
        {
            // Act
            Container.RegisterType(typeof(Service), new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegisterType_Type_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType(typeof(Service), manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_Type_Name()
        {
            // Act
            Container.RegisterType(typeof(Service), Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
        }

        [TestMethod]
        public void RegisterType_Type_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType(typeof(Service), Name, manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_Type_Type()
        {
            // Act
            Container.RegisterType(typeof(IService), typeof(Service), new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
        }

        [TestMethod]
        public void RegisterType_Type_Type_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType(typeof(IService), typeof(Service), manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterType_Type_Type_Name()
        {
            // Act
            Container.RegisterType(typeof(IService), typeof(Service), Name, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
        }

        [TestMethod]
        public void RegisterType_Type_Type_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterType(typeof(IService), typeof(Service), Name, manager, new InjectionConstructor());

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(Service), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion

        #endregion

    }
}
