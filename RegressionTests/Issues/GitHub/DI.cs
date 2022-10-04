using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Lifetime;
#endif

namespace Issues
{
    public partial class GitHub
    {
        [TestMethod]
        public void unitycontainer_microsoft_dependency_injection_14()
        {
            var c1 = Container.CreateChildContainer();
            var c2 = Container.CreateChildContainer();

            c1.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t1 = c1.Resolve<IList<int>>();
            Assert.IsNotNull(t1);

            c2.RegisterType(typeof(IList<>), typeof(List<>), new ContainerControlledLifetimeManager(),
                                                             new InjectionConstructor());
            var t2 = c2.Resolve<IList<int>>();
            Assert.IsNotNull(t2);

            Assert.AreNotSame(t2, t1);

        }
    }
}
