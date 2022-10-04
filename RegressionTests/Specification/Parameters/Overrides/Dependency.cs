using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Overrides_DependencyLegacy()
        {
            // arrange
            Container.RegisterType<IController, TheController>()
                     .RegisterType<IMessageProvider, DefaultMessageProvider>()
                     .RegisterType<IMessageProvider, AlternativeMessageProvider>(nameof(AlternativeMessageProvider));

            var resolvedParameter = new ResolvedParameter<IMessageProvider>(nameof(AlternativeMessageProvider));
            var dependencyOverride = new DependencyOverride<IMessageProvider>(resolvedParameter);

            // act
            var actual = Container.Resolve<IController>(dependencyOverride);

            // assert
            Assert.AreEqual(actual.GetMessage(), "Goodbye cruel world!");
        }
    }
}
