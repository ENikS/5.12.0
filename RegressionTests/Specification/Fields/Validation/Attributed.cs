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
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_AttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_OptionalAttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_AttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_OptionalAttributeOnReadOnly()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeReadOnlyType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_AttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_AttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_OptionalAttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributePrivateType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Validation_OptionalAttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeProtectedType>();
        }
    }
}
