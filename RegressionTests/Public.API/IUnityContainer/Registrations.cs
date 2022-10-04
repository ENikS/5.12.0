using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
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
        public void Registrations()
        {
            Assert.IsNotNull(Container.Registrations);
        }

        [TestMethod]
        public void Registrations_ToEnumerable()
        {
#if NET46 || NET461 
            Assert.IsNotNull(Container.Registrations as IEnumerable<IContainerRegistration>);
#else
            Assert.IsNotNull(Container.Registrations as IEnumerable<ContainerRegistration>);
#endif
        }

        [TestMethod]
        public void Registrations_ToArray()
        {
            var array = Container.Registrations.ToArray();

            Assert.IsNotNull(array);
            Assert.AreNotEqual(0, array.Length);
        }


        [TestMethod]
        public virtual void Registrations_Contains_IUnityContainer()
        {
            // Act
            var registration = Container.Registrations.FirstOrDefault(r => typeof(IUnityContainer) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration);
        }

    }
}
