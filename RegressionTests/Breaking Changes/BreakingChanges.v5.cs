using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Breaking.Changes
{
    [TestClass]
    public class BreakingChangesV5
    {
        #region Scaffolding

        IUnityContainer Container;
        readonly ContainerRegistrationComparer EqualityComparer = new ContainerRegistrationComparer();

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();

        #endregion

        /// <summary>
        /// Test case where collection is changed while being enumerated
        /// </summary>
        /// <remarks>
        /// Acceding to documentation it should throw if collection changes.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Registrations_Enumerable_Is_Immutable()
        {
            Container.RegisterType<IService, Service>()
                     .RegisterType<IService, Service>("second");

            var enumerable = Container.Registrations;

            var registrations1 = enumerable.ToArray();

            Container.RegisterType<Service>()
                     .RegisterType<Service>("second");

            var registrations2 = enumerable.ToArray();

            Assert.IsTrue(registrations1.SequenceEqual(registrations2, EqualityComparer));
            Assert.IsFalse(registrations1.SequenceEqual(Container.Registrations, EqualityComparer));
        }

        /// <summary>
        /// This method demonstrates a case where <see cref="Type"/> mapping created
        /// with <see cref="InjectionConstructor"/> failed to build. 
        /// </summary>
        /// <remarks>
        /// When any injection members are present, the mapping should never redirect to 
        /// other registrations, instead it should create its own pipeline using 
        /// provided <see cref="InjectionMember"/> instances.
        /// </remarks>
        [TestMethod]
        public void Always_Build_If_Injected()
        {
            var instance = new Service<object>();

            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(Service<>), new InjectionConstructor(typeof(object)))
                     .RegisterInstance(instance);
            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            // Should never be the same
            Assert.AreNotSame(instance, value);
        }
    }
}
