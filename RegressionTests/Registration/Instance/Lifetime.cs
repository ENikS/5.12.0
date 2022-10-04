using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class RegisterInstance
    {
        [TestMethod]
        public void PerContainerAtRoot()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, service, new ContainerControlledLifetimeManager());


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void PerContainerAtChild()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ContainerControlledLifetimeManager());
            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ContainerControlledLifetimeManager());
            child2.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ContainerControlledLifetimeManager());


            // Act/Verify

            Assert.AreNotSame(service, Container.Resolve<IService>());
            Assert.AreNotSame(service, child1.Resolve<IService>());
            Assert.AreNotSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void PerContainerThrows()
        {
            // Arrange
            var child1 = Container.CreateChildContainer();

            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ContainerControlledLifetimeManager());

            // Act/Verify
            var result = Container.Resolve<IService>();
        }


        [TestMethod]
        public void ExternalAtRoot()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, service, new ExternallyControlledLifetimeManager());


            // Act/Verify

            Assert.AreSame(service, Container.Resolve<IService>());
            Assert.AreSame(service, child1.Resolve<IService>());
            Assert.AreSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        public void ExternalAtChild()
        {
            // Arrange
            var service = Unresolvable.Create();

            var child1 = Container.CreateChildContainer();
            var child2 = child1.CreateChildContainer();

            Container.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ExternallyControlledLifetimeManager());
            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ExternallyControlledLifetimeManager());
            child2.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ExternallyControlledLifetimeManager());


            // Act/Verify

            Assert.AreNotSame(service, Container.Resolve<IService>());
            Assert.AreNotSame(service, child1.Resolve<IService>());
            Assert.AreNotSame(service, child2.Resolve<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ExternalThrows()
        {
            // Arrange
            var child1 = Container.CreateChildContainer();

            child1.RegisterInstance(typeof(IService), null, Unresolvable.Create(), new ExternallyControlledLifetimeManager());

            // Act/Verify
            var result = Container.Resolve<IService>();
        }
    }
}
