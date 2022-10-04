using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    [TestClass]
    public partial class Parameters
    {
        protected const string Name = "name";
        protected const string Unnamed = "unnamed";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Unnamed);
            Container.RegisterInstance(Name, Name);
        }
    }

    // TODO: [TestClass]
    public partial class Parameters_Diagnostic : Parameters
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
