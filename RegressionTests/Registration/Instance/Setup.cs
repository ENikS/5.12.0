using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Registrations
{
    [TestClass]
    public partial class RegisterInstance
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }

        #region Test Data

    public interface IService
    {
    }

    public class Service : IService 
    {
    }

    public class Unresolvable : IService
    {
        private Unresolvable() { }

        public static Unresolvable Create() => new Unresolvable();
    }


    #endregion
    }
}
