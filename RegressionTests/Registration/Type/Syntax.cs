using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Registrations
{
    public partial class RegisterType
    {
        [TestMethod]
        public void InvalidRegistration()
        {
            // Act
            Container.RegisterType<IService>();
        }

        [TestMethod]
        public void Singleton()
        {
            Container.RegisterSingleton<IService, Service>();
            Container.RegisterType<IService, Service>(Name, new ContainerControlledLifetimeManager());

            // Act
            var anonymous = Container.Resolve<IService>();
            var named = Container.Resolve<IService>(Name);

            // Validate
            Assert.IsNotNull(anonymous);
            Assert.IsNotNull(named);
            Assert.AreNotSame(anonymous, named);
        }

    }
}
