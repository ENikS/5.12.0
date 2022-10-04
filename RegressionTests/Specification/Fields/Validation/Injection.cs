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
    public partial class Fields_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_NoReuse()
        {
            // Arrange
            var field = Inject.Field(nameof(DependencyInjectedType.NormalField), "test");

            // Act
            Container.RegisterType<DependencyInjectedType>("1", field)
                     .RegisterType<DependencyInjectedType>("2", field);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_InjectReadOnlyField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field(nameof(DependencyInjectedType.ReadonlyField), "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_InjectPrivateField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field("PrivateField", "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_InjectProtectedField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field("ProtectedField", "test"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_InjectStaticField()
        {
            // Act
            Container.RegisterType<DependencyInjectedType>(
                Inject.Field(nameof(DependencyInjectedType.StaticField), "test"));
        }
    }
}
