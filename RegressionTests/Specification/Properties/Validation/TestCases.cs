using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Properties_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidValue()
        {
            // Act
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Container), Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.ReadOnlyProperty), "test"));
        }
    }
}
