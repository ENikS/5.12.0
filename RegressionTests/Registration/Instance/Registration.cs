using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class RegisterInstance
    {
        [TestMethod]
        public void IsNotNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), Unresolvable.Create(), new ContainerControlledLifetimeManager());
            
            // Validate
            Assert.IsNotNull(Container.Resolve<IService>());
        }

        [TestMethod]
        public void IsNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), (object)null, new ContainerControlledLifetimeManager());

            // Validate
            Assert.IsNull(Container.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void ThrowsOnNullNull()
        {
            Container.RegisterInstance(null, null, (object)null, new ContainerControlledLifetimeManager());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Null_Null_Null()
        {
            // Act
#if V4
            Container.RegisterInstance(null, null, null, (LifetimeManager)null);
#else
            Container.RegisterInstance(null, null, null, (IInstanceLifetimeManager)null);
#endif
        }

        [TestMethod]
        public void Null_Null_Instance()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(value);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.AreSame(value, instance);
        }

        [TestMethod]
        public void Null_Name_Instance()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(Name, value);

            // Act
            var instance = Container.Resolve<object>(Name);

            // Validate
            Assert.AreSame(value, instance);
        }

        [TestMethod]
        public void Type_Null_Null()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), (object)null);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.IsNull(instance);
        }

        [TestMethod]
        public void Type_Null_Instance()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), (object)Name);

            // Act
            var instance = Container.Resolve<object>();

            // Validate
            Assert.AreSame(Name, instance);
        }

        [TestMethod]
        public void Type_Name_Instance()
        {
            // Arrange
            Container.RegisterInstance(typeof(object), Name, Name);

            // Act
            var instance = Container.Resolve<object>(Name);

            // Validate
            Assert.AreSame(Name, instance);
        }

        [TestMethod]
        public void DefaultLifetime()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(typeof(object), null, value);

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

        [TestMethod]
        public void CanSetLifetime()
        {
            // Arrange
            var value = new object();
            Container.RegisterInstance(typeof(object), null, value, new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }
    }
}
