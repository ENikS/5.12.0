using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {
        [TestMethod]
        public void Override_DependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>(new DependencyOverride(typeof(string), _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }

#if !NET45
        [TestMethod]
        public void Override_NamedDependencyLegacy()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>(new DependencyOverride(typeof(string), Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }

        [TestMethod]
        public void Override_NamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>(Override.Dependency<string>(Name, _override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }
#endif

        [TestMethod]
        public void Override_Dependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>(new DependencyOverride<string>(_override));

            // Validate
            Assert.AreEqual(_override, instance.Data);
        }
    }
}
