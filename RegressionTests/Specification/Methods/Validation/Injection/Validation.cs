using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods_Diagnostic
    {
#if !NET45
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_MultipleMethods() => base.Injection_MultipleMethods();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_ReturningInt() => base.Injection_ReturningInt();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_MethodPassingVoid() => base.Injection_MethodPassingVoid();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_ReturningVoid() => base.Injection_ReturningVoid();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_StaticMethod() => base.Injection_StaticMethod();
#endif
    }
}
