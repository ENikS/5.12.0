using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Registrations
{
    public partial class Lifetime
    {
        [TestMethod]
        public void PerThread_Type_SameThread()
        {
            Container.RegisterType<IService, Service>(new PerThreadLifetimeManager());

            var a = Container.Resolve<IService>();
            var b = Container.Resolve<IService>();

            Assert.AreSame(a, b);
        }

        [TestMethod]
        public void PerThread_Type_DifferentThreads()
        {
            Container.RegisterType<IService, Service>(new PerThreadLifetimeManager());

            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadProcedure));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadProcedure));

            ThreadInformation info = new ThreadInformation(Container);

            t1.Start(info);
            t2.Start(info);
            t1.Join();
            t2.Join();

            var a = new List<IService>(info.ThreadResults.Values)[0];
            var b = new List<IService>(info.ThreadResults.Values)[1];

            Assert.AreNotSame(a, b);
        }

        [TestMethod]
        public void PerThread_Factory_SameThread()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), new PerThreadLifetimeManager());

            var a = Container.Resolve<IService>();
            var b = Container.Resolve<IService>();

            Assert.AreSame(a, b);
        }

        [TestMethod]
        public void PerThread_Factory_DifferentThreads()
        {
            Container.RegisterFactory<IService>((c, t, n) => new Service(), new PerThreadLifetimeManager());

            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadProcedure));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadProcedure));

            ThreadInformation info = new ThreadInformation(Container);

            t1.Start(info);
            t2.Start(info);
            t1.Join();
            t2.Join();

            var a = new List<IService>(info.ThreadResults.Values)[0];
            var b = new List<IService>(info.ThreadResults.Values)[1];

            Assert.AreNotSame(a, b);
        }
    }
}
