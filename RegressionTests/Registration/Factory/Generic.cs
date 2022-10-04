using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Registrations
{
    public partial class RegisterFactory
    {
        [TestMethod]
        public void FactoryOpenGeneric()
        {
            // Arrange
            Container.RegisterFactory(typeof(IFoo<>), (c, t, n) => new Foo<object>());

            // Act
            var result = Container.Resolve(typeof(IFoo<object>));

            // Verify
            Assert.IsNotNull(result);
        }
    }
}
