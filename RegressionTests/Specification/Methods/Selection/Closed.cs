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
        public void Selection_Method_Called()
        {
            // Act
            var result = Container.Resolve<InjectedMethodTest>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ExecutedVoid);
            Assert.AreEqual((object) Name, result.Executed);
        }

        [TestMethod]
        public void Selection_Method_Called_Mapped()
        {
            // Act
            var result = Container.Resolve<IInjectedMethodTest>();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((object) Name, result.Executed);
        }
    }
}
