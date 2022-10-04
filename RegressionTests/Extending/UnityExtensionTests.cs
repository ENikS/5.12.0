using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Extension;
using Unity;
#endif


namespace Extending
{
    [TestClass]
    public class UnityExtensionTests
    {
        UnityContainer            container;
        MockContainerExtension    mock;
        OtherContainerExtension   other;
        DerivedContainerExtension derived;

        [TestInitialize]
        public void TestInitialize()
        {
            container = new UnityContainer();
            mock      = new MockContainerExtension();
            other     = new OtherContainerExtension();
            derived   = new DerivedContainerExtension();
        }

        [TestMethod]
        public void AddExtensionTest()
        {
            // Act
            container.AddExtension(mock)
                     .AddExtension(other)
                     .AddExtension(derived);

            // Validate
            Assert.IsTrue(mock.InitializeWasCalled);
            Assert.IsTrue(other.InitializeWasCalled);
            Assert.IsTrue(derived.InitializeWasCalled);
        }

        [TestMethod]
        public void ConfigureOrderTest()
        {
            // Act
            container.AddExtension(mock)
                     .AddExtension(other)
                     .AddExtension(derived);

            // Validate
            Assert.AreSame(derived, container.Configure(typeof(DerivedContainerExtension)));
            Assert.AreSame(mock,    container.Configure(typeof(MockContainerExtension)));
        }

        [TestMethod]
        public void ConfigureOrderReversed()
        {
            // Act
            container.AddExtension(derived)
                     .AddExtension(other)
                     .AddExtension(mock);

            // Validate
            Assert.AreSame(derived, container.Configure(typeof(DerivedContainerExtension)));
            Assert.AreSame(derived, container.Configure(typeof(MockContainerExtension)));
        }

        [TestMethod]
        public void ConfigureGenericTest()
        {
            // Act
            container.AddExtension(mock)
                     .AddExtension(derived);

            // Validate
            Assert.AreSame(derived, container.Configure<DerivedContainerExtension>());
            Assert.AreSame(mock,    container.Configure<MockContainerExtension>());
        }

        [TestMethod]
        public void AddExtensionGenericTest()
        {
            // Act
            container.AddNewExtension<MockContainerExtension>()
                     .AddNewExtension<DerivedContainerExtension>();

            // Validate
            Assert.IsTrue(container.Configure<MockContainerExtension>().InitializeWasCalled);
            Assert.IsTrue(container.Configure<DerivedContainerExtension>().InitializeWasCalled);
        }
    }
}
