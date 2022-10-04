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
    public partial class Parameters_BuiltUp : Specification.Parameters
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Parameters_Compiled : Specification.Parameters
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Parameters_Activated : Specification.Parameters
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }

    [TestClass]
    public partial class Parameters_BuiltUp_Diagnostic : Specification.Parameters_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Parameters_Compiled_Diagnostic : Specification.Parameters_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }

    }

    [TestClass]
    public partial class Parameters_Activated_Diagnostic : Specification.Parameters_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }

    }
}
