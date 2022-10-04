using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class Hierarchy
    {
        [TestMethod]
        public void ParametersAtRootTransient()
        {
            // Arrange
            Container.RegisterType(typeof(MultiLevelType), new TransientLifetimeManager());

            // Act
            var result0 = iUnity0.Resolve<MultiLevelType>();
            var result5 = iUnity5.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result0);
            Assert.IsNotNull(result5);

            Assert.AreNotSame(result0, result5);

            Assert.AreEqual(Container, result0.Container);
            Assert.AreEqual(Container, result5.Container);

            Assert.AreEqual(0, result0.Level);
            Assert.AreEqual(0, result5.Level);
        }

        [TestMethod]
        public void ParametersAtRootPerContainer()
        {
            // Arrange
            Container.RegisterType(typeof(MultiLevelType), new ContainerControlledLifetimeManager());

            // Act
            var result0 = iUnity0.Resolve<MultiLevelType>();
            var result5 = iUnity5.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result0);
            Assert.IsNotNull(result5);

            Assert.AreSame(result0, result5);

            Assert.AreEqual(Container, result0.Container);
            Assert.AreEqual(Container, result5.Container);

            Assert.AreEqual(0, result0.Level);
            Assert.AreEqual(0, result5.Level);
        }

        [TestMethod]
        public void ParametersAtChildTransient()
        {
            // Arrange
            iUnity2.RegisterType(typeof(MultiLevelType), new TransientLifetimeManager());

            // Act
            var result2 = iUnity2.Resolve<MultiLevelType>();
            var result5 = iUnity5.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result5);

            Assert.AreNotSame(result2, result5);

            Assert.AreEqual(iUnity2, result2.Container);
            Assert.AreEqual(iUnity2, result5.Container);

            Assert.AreEqual(2, result2.Level);
            Assert.AreEqual(2, result5.Level);
        }

        [TestMethod]
        public void ParametersAtChildPerContainer()
        {
            // Arrange
            iUnity2.RegisterType(typeof(MultiLevelType), new ContainerControlledLifetimeManager());

            // Act
            var result0 = iUnity0.Resolve<MultiLevelType>();
            var result2 = iUnity2.Resolve<MultiLevelType>();
            var result5 = iUnity5.Resolve<MultiLevelType>();

            // Validate
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result5);

            Assert.AreSame(result2, result5);

            Assert.AreEqual(iUnity2, result2.Container);
            Assert.AreEqual(iUnity2, result5.Container);

            Assert.AreEqual(2, result2.Level);
            Assert.AreEqual(2, result5.Level);
        }
    }
}
