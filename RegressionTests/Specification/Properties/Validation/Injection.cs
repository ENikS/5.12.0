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
    public partial class Properties_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoReuse()
        {
            // Arrange
            var property = Inject.Property(nameof(DependencyInjectedType.NormalProperty), "test");

            // Act
            Container.RegisterType<DependencyInjectedType>("1", property)
                     .RegisterType<DependencyInjectedType>("2", property);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectReadOnlyProperty()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Property(nameof(DependencyInjectedType.ReadonlyProperty), "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectPrivateProperty()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Property("PrivateProperty", "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectProtectedProperty()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Property("ProtectedProperty", "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InjectStaticProperty()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Property(nameof(DependencyInjectedType.StaticProperty), "test"));
        }
    }
}
