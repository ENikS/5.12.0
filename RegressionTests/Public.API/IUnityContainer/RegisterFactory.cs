using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
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
        protected Func<IUnityContainer, object> ShortFactory = (c) => c;
        protected Func<IUnityContainer, Type, string, object> FullFactory = (c, t, n) => c;

        [TestMethod]
        public void RegisterFactrory()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #region RegisterFactory overloads


        #region Generics Short Factory overloads

        [TestMethod]
        public void RegisterFactrory_C_T()
        {
            // Act
            Container.RegisterFactory<IService>(ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_C_T_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_C_T_Name()
        {
            // Act
            Container.RegisterFactory<IService>(Name, ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_C_T_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(Name, ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Generics Full Factory overloads

        [TestMethod]
        public void RegisterFactrory_CTN_T()
        {
            // Act
            Container.RegisterFactory<IService>(FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_CTN_T_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_CTN_T_Name()
        {
            // Act
            Container.RegisterFactory<IService>(Name, FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_CTN_T_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory<IService>(Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Non-generic overloads


        #region Short Factory overloads

        [TestMethod]
        public void RegisterFactrory_C_Type()
        {
            // Act
            Container.RegisterFactory(typeof(IService), ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_C_Type_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_C_Type_Name()
        {
            // Act
            Container.RegisterFactory(typeof(IService), Name, ShortFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_C_Type_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, ShortFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #region Full Factory overloads

        [TestMethod]
        public void RegisterFactrory_CTN_Type()
        {
            // Act
            Container.RegisterFactory(typeof(IService), FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_CTN_Type_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.IsNull(registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        [TestMethod]
        public void RegisterFactrory_CTN_Type_Name()
        {
            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void RegisterFactrory_CTN_Type_Name_Manager()
        {
            // Arrange
            var manager = new TransientLifetimeManager();

            // Act
            Container.RegisterFactory(typeof(IService), Name, FullFactory, manager);

            // Validate
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.RegisteredType);
            Assert.AreEqual(typeof(IService), registration.MappedToType);
            Assert.AreEqual(Name, registration.Name);
            Assert.AreSame(manager, registration.LifetimeManager);
        }

        #endregion


        #endregion

        #endregion
    }
}
