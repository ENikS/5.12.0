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
    public partial class Constructors
    {
        /// <summary>
        /// The BuildUp does not throw anymore. We do not care if 
        /// constructor is invalid, we are not calling it
        /// </summary>
        [TestMethod]
#if V4
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public void BuildUp_AmbuguousConstructor()
        {
            // Arrange
            var instance = new TypeWithAmbiguousCtors();

            // Act
            Container.BuildUp(instance);

            // Assert
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
            Assert.AreEqual(Container,                  instance.Container);
        }

        /// <summary>
        /// The BuildUp does not throw anymore. We do not care if 
        /// constructor is invalid, we are not calling it
        /// </summary>
        [TestMethod]
#if V4
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public void BuildUp_AmbuguousAnnotations()
        {
            // Arrange
            var instance = new TypeWithAmbuguousAnnotations();

            // Act
            Container.BuildUp(instance);

            // Assert
            Assert.AreEqual(1,         instance.Ctor);
            Assert.AreEqual(Container, instance.Container);
        }
    }
}
