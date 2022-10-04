using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Fields_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Cyclic_FieldToInterface()
        {
            // Arrange
            Container.RegisterType<I1, D1>();

            // Act
            Container.Resolve<D1>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Cyclic_DependencyOverride()
        {
            // Arrange
            Container.RegisterType<I0, G0>()
                     .RegisterType<I1, G1>();

            //next line throws StackOverflowException
            Container.Resolve<G1>(
                Override.Dependency<I0>(
                    Resolve.Dependency<I1>()));
        }
    }
}
