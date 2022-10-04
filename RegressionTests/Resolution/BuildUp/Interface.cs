using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Resolution
{
    public partial class BuildUp
    {
        [TestMethod]
        public void BuildUpInterfaceWithDependency()
        {
            // Setup
            BarClass objBase = new BarClass();

            // Act
            Container.BuildUp(typeof(IFooInterface), objBase);

            // Verify
            Assert.IsNotNull(objBase.InterfaceProp);
        }

        [TestMethod]
        public void BuildUpInterfaceWithoutDependency()
        {
            // Setup
            BarClass2 objBase = new BarClass2();

            // Act
            Container.BuildUp(typeof(IFooInterface2), objBase);

            // Verify
            Assert.IsNull(objBase.InterfaceProp);
        }
    }
}
