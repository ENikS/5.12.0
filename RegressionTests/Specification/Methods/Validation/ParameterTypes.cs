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
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ChainedExecuteMethodBaseline()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionMethod("ChainedExecute"));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.Fail();
        }
#endif
    }
}
