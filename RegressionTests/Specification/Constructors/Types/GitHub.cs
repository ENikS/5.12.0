using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
#endif

namespace Specification
{
    public partial class Constructors_Diagnostic
    {
        [Ignore]  // TODO: requires fix
        [TestMethod]
        public void Unity_54()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                container.RegisterType(typeof(IRecInterface), typeof(RecClass));
                container.RegisterInstance(new RecClass());
                var instance = container.Resolve<IRecInterface>(); //0
                Assert.IsNotNull(instance);
            }

            using (IUnityContainer container = new UnityContainer())
            {
                container.RegisterType(typeof(IRecInterface), typeof(RecClass));
                container.RegisterType<RecClass>(new ContainerControlledLifetimeManager());

                try
                {
                    var instance = container.Resolve<IRecInterface>(); //2
                    Assert.Fail("Should throw an exception");
                }
                catch (ResolutionFailedException e)
                {
                    Assert.IsInstanceOfType(e, typeof(ResolutionFailedException));
                }

            }
        }
    }
}
