using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Registrations
{
    public partial class Lifetime
    {
        [TestMethod]
        public void Transient_Factory_Null()
        {
            // Arrange
            Container.RegisterFactory<IService>(c => null);

            // Act
            var instance = Container.Resolve<IService>();

            // Validate
            Assert.IsNull(instance);
        }
    }
}
