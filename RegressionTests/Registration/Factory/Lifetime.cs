using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class RegisterFactory
    {
        [TestMethod]
        public void Factory_Hierarchical()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), new HierarchicalLifetimeManager());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, Container.Resolve<IService>());

            using (var child = Container.CreateChildContainer())
            {
                Assert.AreNotSame(service, child.Resolve<IService>());
            }
        }

        [TestMethod]
        public void Factory_Singleton()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), new ContainerControlledLifetimeManager());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, Container.Resolve<IService>());
        }

        [TestMethod]
        public void Factory_Transient()
        {
            var foo = new Service();
            Container.RegisterFactory<IService>((c, t, n) => foo);

            var service = Container.Resolve<IService>();
            var repeat = Container.Resolve<IService>();

            Assert.IsNotNull(service);
            Assert.AreSame(service, foo);
            Assert.AreSame(service, repeat);
        }
    }
}
