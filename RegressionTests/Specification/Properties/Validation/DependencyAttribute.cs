using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Properties
    {
        [TestMethod]
        public virtual void Annotation_DependencyAttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public virtual void Annotation_DependencyAttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.IsNull(result.Value);
        }
    }

    public partial class Properties_Diagnostic : Properties
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyAttributeOnPrivate()
        {
            base.Annotation_DependencyAttributeOnPrivate();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyAttributeOnProtected()
        {
            base.Annotation_DependencyAttributeOnProtected();
        }
    }
}
