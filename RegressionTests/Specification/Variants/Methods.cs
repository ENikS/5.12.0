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
    public partial class Methods_BuiltUp : Specification.Methods
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Methods_Compiled : Specification.Methods
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Methods_Activated : Specification.Methods
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }

    [TestClass]
    public partial class Methods_BuiltUp_Diagnostic : Specification.Methods_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Methods_Compiled_Diagnostic : Specification.Methods_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Methods_Activated_Diagnostic : Specification.Methods_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }
}
