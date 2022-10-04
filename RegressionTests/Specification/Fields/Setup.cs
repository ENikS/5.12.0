using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    [TestClass]
    public partial class Fields
    {
        protected const string Name = "name";
        protected const string Name1 = "name1";
        protected const string Name2 = "name2";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
            Container.RegisterInstance(Name, Name);
            Container.RegisterInstance(Name1, Name1);
            Container.RegisterInstance(Name2, Name2);
        }
    }

    // TODO: [TestClass]
    public partial class Fields_Diagnostic : Fields
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
