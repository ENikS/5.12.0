using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification.Variants
{
    [TestClass]
    public partial class Properties_BuiltUp : Specification.Properties
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Properties_Compiled : Specification.Properties
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Properties_Activated : Specification.Properties
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }

    [TestClass]
    public partial class Properties_BuiltUp_Diagnostic : Specification.Properties_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Properties_Compiled_Diagnostic : Specification.Properties_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Properties_Activated_Diagnostic : Specification.Properties_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }
}
