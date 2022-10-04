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
        IService Instance = new Service();

        [TestMethod]
        public void RegisterInstance()
        {
            // Arrange
            var typeFrom = typeof(IService);
            var manager = new ContainerControlledLifetimeManager();

            // Act
            Container.RegisterInstance(typeFrom, Name, Instance, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeFrom, registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #region RegisterInstance overloads

        #region Generics overloads

        [TestMethod]
        public virtual void RegisterInstance_T()
        {
            // Act
            Container.RegisterInstance<IService>(Instance);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void RegisterInstance_T_Manager()
        {
            // Arrange
            var manager = new ContainerControlledLifetimeManager();

            // Act
            Container.RegisterInstance<IService>(Instance, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterInstance_T_Name()
        {
            // Act
            Container.RegisterInstance<IService>(Name, Instance);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void RegisterInstance_T_Name_Manager()
        {
            // Arrange
            var manager = new ContainerControlledLifetimeManager();

            // Act
            Container.RegisterInstance<IService>(Name, Instance, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion

        #region Non-generics overloads

        [TestMethod]
        public virtual void RegisterInstance_Type()
        {
            // Act
            Container.RegisterInstance(typeof(IService), Instance);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void RegisterInstance_Type_Manager()
        {
            // Arrange
            var manager = new ContainerControlledLifetimeManager();

            // Act
            Container.RegisterInstance(typeof(IService), Instance, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void RegisterInstance_Type_Name()
        {
            // Act
            Container.RegisterInstance(typeof(IService), Name, Instance);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void RegisterInstance_Type_Name_Manager()
        {
            // Arrange
            var manager = new ContainerControlledLifetimeManager();

            // Act
            Container.RegisterInstance(typeof(IService), Name, Instance, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion

        #endregion

    }
}
