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
    public partial class Methods
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
            Container.RegisterType(typeof(IInjectedMethodTest), typeof(InjectedMethodTest));
            Container.RegisterType(typeof(IGenericInjectedMethodTest<>), typeof(GenericInjectedMethodTest<>));
        }
    }


    // TODO: [TestClass]
    public partial class Methods_Diagnostic : Methods
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
