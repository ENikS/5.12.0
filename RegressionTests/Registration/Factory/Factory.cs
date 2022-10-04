using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
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
        public void ShortSignature()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void LongSignature()
        {
            Container.RegisterFactory<IService>(c => new Service());

            var service = Container.Resolve<IService>();

            Assert.IsNotNull(service);
        }
    }
}
