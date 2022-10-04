using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Fields
    {
        [TestMethod]
        public void Overrides_FieldOverrideAttribute()
        {
            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), null),
                Override.Field(nameof(ObjectWithAttributes.Optional), Name1));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNotNull(result.Optional);
            Assert.AreEqual(result.Optional, Name1);
        }

        [TestMethod]
        public void Overrides_DependencyOverrideFieldValue()
        {
            var other = "other";

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), null),
                Override.Dependency(other, other));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNotNull(result.Optional);
            Assert.AreEqual(result.Optional, other);
        }

        [TestMethod]
        public void Overrides_ValueOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(Override.Field(nameof(ObjectWithAttributes.Dependency), Name));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void Overrides_NullOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(Override.Field(nameof(ObjectWithAttributes.Dependency), null));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Dependency);
            Assert.IsNull(result.Optional);
        }

    }
}
