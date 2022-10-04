using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Lifetime;
using Unity;
#endif


namespace Extending
{
    [TestClass]
    public class ChildContainerTests
    {
        IUnityContainer Container;
        MockContainerExtension extension1;
        MockContainerExtension extension2;
        UnrelatedExtension     extension3;

        [TestInitialize]
        public void TestInitialize()
        {
            Container  = new UnityContainer();
            extension1 = new MockContainerExtension();
            extension2 = new MockContainerExtension();
            extension3 = new UnrelatedExtension();
        }

        [TestMethod]
        public void Baseline()
        {
            // Act
            Container.AddExtension(extension1);

            // Validate
            Assert.AreSame(Container, extension1.ExtensionContext.Container);
            Assert.IsNotNull(extension1.ExtensionContext.Policies);
        }

        [TestMethod]
        public void ExtensionInChild()
        {
            // Act
            var level_two = Container.AddExtension(extension1)
                                     .CreateChildContainer()
                                     .AddExtension(extension2);
            // Validate
            Assert.AreSame(Container, extension1.ExtensionContext.Container);
            Assert.AreSame(level_two, extension2.ExtensionContext.Container);

            Assert.AreNotSame(extension1.ExtensionContext.Container,
                              extension2.ExtensionContext.Container);

            Assert.IsNotNull(extension1.ExtensionContext.Policies);
            Assert.IsNotNull(extension2.ExtensionContext.Policies);
        }

        [TestMethod]
        public void ExtensionsAreNotShared()
        {
            // Act
            var level_two = Container.AddExtension(extension1)
                                     .CreateChildContainer()
                                     .AddExtension(extension3);
            // Validate
            Assert.IsNotNull(level_two.Configure(typeof(UnrelatedExtension)));
            Assert.IsNotNull(Container.Configure<MockContainerExtension>());

            Assert.IsNull(level_two.Configure<MockContainerExtension>());
            Assert.IsNull(Container.Configure(typeof(UnrelatedExtension)));
        }
    }
}
