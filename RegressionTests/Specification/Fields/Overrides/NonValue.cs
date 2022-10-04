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
        public void Overrides_FieldOverrideAttributeResolver()
        {
            // Act
            var resolver1 = new ValidatingResolver(Name);
            var resolver2 = new ValidatingResolver(Name1);
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), resolver1),
                Override.Field(nameof(ObjectWithAttributes.Optional), resolver2));

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dependency, Name);
            Assert.AreEqual(result.Optional, Name1);

            Assert.AreEqual(typeof(string), resolver1.Type);
            Assert.AreEqual(typeof(string), resolver2.Type);
        }

        [TestMethod]
        public void Overrides_DependencyOverrideFieldValueResolver()
        {
            var other = "other";
            var resolver1 = new ValidatingResolver(Name);

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), resolver1),
                Override.Dependency(other, other));

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dependency, Name);
            Assert.AreEqual(result.Optional, other);

            Assert.AreEqual(typeof(string), resolver1.Type);
        }

        [TestMethod]
        public void Overrides_FieldOverrideAttributeFactory()
        {
            // Act
            var resolver1 = new ValidatingResolverFactory(Name);
            var resolver2 = new ValidatingResolverFactory(Name1);
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), resolver1),
                Override.Field(nameof(ObjectWithAttributes.Optional), resolver2));

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dependency, Name);
            Assert.AreEqual(result.Optional, Name1);

            Assert.AreEqual(typeof(string), resolver1.Type);
            Assert.AreEqual(typeof(string), resolver2.Type);
            Assert.AreEqual("name1", resolver1.Name);
            Assert.AreEqual("other", resolver2.Name);
        }

        [TestMethod]
        public void Overrides_DependencyOverrideFieldValueFactory()
        {
            var other = "other";
            var resolver1 = new ValidatingResolverFactory(Name);

            // Act
            var result = Container.Resolve<ObjectWithAttributes>(
                Override.Field(nameof(ObjectWithAttributes.Dependency), resolver1),
                Override.Dependency(other, other));

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dependency, Name);
            Assert.AreEqual(result.Optional, other);

            Assert.AreEqual(typeof(string), resolver1.Type);
            Assert.AreEqual("name1", resolver1.Name);
        }

    }
}
