using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods
    {
        [TestMethod]
        public void Selection_Method_Called_Generic()
        {
            // Act
            var result = Container.Resolve<GenericInjectedMethodTest<string>>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ExecutedVoid);
            Assert.AreEqual((object) Name, result.ExecutedGeneric);
        }

        [TestMethod]
        public void Selection_Method_Called_Mapped_Generic()
        {
            // Act
            var result = Container.Resolve<IGenericInjectedMethodTest<string>>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((object) Name, result.ExecutedGeneric);
        }
    }
}
