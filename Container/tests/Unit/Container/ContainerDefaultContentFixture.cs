using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Tests.v5.Container
{
    [TestClass]
    public class ContainerDefaultContentFixture
    {
        [TestMethod]
        public void WhenResolvingAnIUnityContainerItResolvesItself()
        {
            IUnityContainer container = new UnityContainer();

            IUnityContainer resolvedContainer = container.Resolve<IUnityContainer>();

            Assert.AreSame(container, resolvedContainer);
        }

        [TestMethod]
        public void WhenResolveingAnIUnityContainerForAChildContainerItResolvesTheChildContainer()
        {
            IUnityContainer container = new UnityContainer();
            IUnityContainer childContainer = container.CreateChildContainer();

            IUnityContainer resolvedContainer = childContainer.Resolve<IUnityContainer>();

            Assert.AreSame(childContainer, resolvedContainer);
        }

        [TestMethod]
        public void AClassThatHasADependencyOnTheContainerGetsItInjected()
        {
            IUnityContainer container = new UnityContainer();

            IUnityContainerInjectionClass obj;
            obj = container.Resolve<IUnityContainerInjectionClass>();

            Assert.AreSame(container, obj.Container);
        }

        public class IUnityContainerInjectionClass
        {
            [Dependency]
            public IUnityContainer Container { get; set; }
        }
    }
}
