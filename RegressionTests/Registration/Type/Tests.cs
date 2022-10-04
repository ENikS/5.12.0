using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Registrations
{
    public partial class RegisterType
    {
        [TestMethod]
        public void DefaultLifetime()
        {
            // Arrange
            Container.RegisterType(typeof(object));

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(TransientLifetimeManager));
        }

        [TestMethod]
        public void CanSetLifetime()
        {
            // Arrange
            Container.RegisterType(typeof(object), new ContainerControlledLifetimeManager());

            // Act
            var registration = Container.Registrations.First(r => typeof(object) == r.RegisteredType);

            // Validate
            Assert.IsInstanceOfType(registration.LifetimeManager, typeof(ContainerControlledLifetimeManager));
        }

    }
}
