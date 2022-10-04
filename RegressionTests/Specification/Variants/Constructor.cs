using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specification;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif


namespace Specification.Variants
{
    [TestClass]
    public partial class Constructors_BuiltUp : Specification.Constructors
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Constructors_Compiled : Specification.Constructors
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Container.AddExtension(new ForceCompillation());

        }
    }

    [TestClass]
    public partial class Constructors_Activated : Specification.Constructors
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Container.AddExtension(new ForceActivation());

        }
    }

    [TestClass]
    public partial class Constructors_BuiltUp_Diagnostic : Specification.Constructors_Diagnostic
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

    }

    [TestClass]
    public partial class Constructors_Compiled_Diagnostic : Specification.Constructors_Diagnostic
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Container.AddExtension(new ForceCompillation());

        }
    }

    [TestClass]
    public partial class Constructors_Activated_Diagnostic : Specification.Constructors_Diagnostic
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Container.AddExtension(new ForceActivation());

        }
    }
}
