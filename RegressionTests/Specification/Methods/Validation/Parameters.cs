using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public partial class Methods_Diagnostic
    {
#if !NET45
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AnonymousTypeForGenericFails()
        {
            // Act
            Container.RegisterType(typeof(GenericService<,,>),
                new InjectionMethod("Method", Resolve.Parameter()));
        }
#endif
    }
}
