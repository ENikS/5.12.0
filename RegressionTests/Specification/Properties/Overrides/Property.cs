using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Properties
    {
        [TestMethod]
        public void Overrides_CanOverrideValue()
        {
            // Act
            var result = Container.Resolve<ObjectWithProperty>(
                Override.Property(nameof(ObjectWithProperty.MyProperty), Resolve.Dependency<ISomething>(Name))
                        .OnType<ObjectWithProperty>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyProperty);
            Assert.IsInstanceOfType(result.MyProperty, typeof(Something2));
        }

        [TestMethod]
        public void Overrides_CanOverrideAttributedValue()
        {
            var other = "other";

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Name), other)
                    .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual<string>(result.Name, other);
        }

        [TestMethod]
        public void Overrides_CanOverridePropOnAttributed()
        {
            // Arrange
            Container.RegisterType<ObjectWithThreeProperties>(
                new InjectionProperty(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var other = "other";
            var result = Container.Resolve<ObjectWithThreeProperties>(
                Override.Property(nameof(ObjectWithThreeProperties.Property), other)
                        .OnType<ObjectWithThreeProperties>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
            Assert.AreEqual((object) other, result.Property);
        }

        [TestMethod]
        public void Overrides_ValueOverrideForTypeDifferentThanResolvedTypeIsIgnored()
        {
            // Act
            var result = Container.Resolve<ObjectWithProperty>(
                Override.Property(nameof(ObjectWithProperty.MyProperty), Resolve.Dependency<ISomething>(Name))
                        .OnType<ObjectThatDependsOnSimpleObject>());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MyProperty);
            Assert.IsInstanceOfType(result.MyProperty, typeof(Something1));
        }
    }
}
