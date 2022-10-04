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
        public virtual void Annotation_DependencyOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public virtual void Annotation_DependencyOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.IsNull(result.Value);
        }
    }

    public partial class Fields_Diagnostic : Fields
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyOnPrivate() 
            => base.Annotation_DependencyOnPrivate();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyOnProtected() 
            => base.Annotation_DependencyOnProtected();
    }
}
