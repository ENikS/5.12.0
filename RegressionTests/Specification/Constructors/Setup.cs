using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    [TestClass]
    public partial class Constructors
    {
        private string _data = "data";
        private string _override = "override";
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }
    }

    // TODO: [TestClass]
    public partial class Constructors_Diagnostic : Constructors
    {
        [TestInitialize]
#if V4
        public override void TestInitialize() => base.TestInitialize();
#else
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new Unity.Diagnostic());
        }
#endif
    }
}
