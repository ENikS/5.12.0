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
    public partial class Fields_BuiltUp : Specification.Fields
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Fields_Compiled : Specification.Fields
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Fields_Activated : Specification.Fields
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }

    [TestClass]
    public partial class Fields_BuiltUp_Diagnostic : Specification.Fields_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Fields_Compiled_Diagnostic : Specification.Fields_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Fields_Activated_Diagnostic : Specification.Fields_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }
}
