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
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnIndex()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeIndexType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnIndex()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeIndexType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void OptionalAttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeProtectedType>();
        }
    }
}
