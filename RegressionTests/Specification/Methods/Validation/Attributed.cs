using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods_Diagnostic
    {

#if !NET45
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnStatic()
        {
            // Act
            var result = Container.Resolve<AttributeStaticType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<AttributePrivateType>();

            Assert.IsTrue(result.Executed);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<AttributeProtectedType>();
        }
#endif

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnOpenGeneric()
        {
            // Act
            var result = Container.Resolve<AttributeOpenGenericType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnOutParam()
        {
            // Act
            var result = Container.Resolve<AttributeOutParamType>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void AttributeOnRefParam()
        {
            // Act
            var result = Container.Resolve<AttributeRefParamType>();
        }
    }
}
