using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity.Extension;
using Unity.Policy;
using Unity;
#endif


namespace Extending
{
    [TestClass]
    public partial class ExtensionContextTests
    {
        ExtensionContext context;   
        UnityContainer container;

        [TestInitialize]
        public void TestInitialize()
        {
            container = new UnityContainer();
            var mock = new MockContainerExtension();
            container.AddExtension(mock);
            context = ((IMockConfiguration)mock).ExtensionContext;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainerNullAddNew()
        {
            UnityContainer unity = null;
            
            unity.AddNewExtension<MockContainerExtension>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainerNullConfigure()
        {
            UnityContainer unity = null;

            unity.Configure<MockContainerExtension>();
        }

        [TestMethod]
        public void ContainerTest()
        {
            // Validate
            Assert.IsNotNull(context.Container);
            Assert.IsInstanceOfType(context.Container, typeof(UnityContainer));
        }

        [TestMethod]
        public void PoliciesTest()
        {
            // Validate
            Assert.IsNotNull(context.Policies);
            Assert.IsInstanceOfType(context.Policies, typeof(IPolicyList));
        }

        [TestMethod]
        public void AddExtensionTest()
        {
            var unity = new UnityContainer();
            var extension = new MockContainerExtension();
            unity.AddExtension(extension);

            Assert.IsTrue(extension.InitializeWasCalled);
            Assert.IsNotNull(((IMockConfiguration)extension).ExtensionContext);
        }

    }
}
