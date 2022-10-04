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
    public partial class Fields_Diagnostic
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_InvalidValue()
        {
            // Act
            Container.RegisterType<ObjectWithThreeFields>(
                Inject.Field(nameof(ObjectWithThreeFields.Container), Name));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validation_ReadOnlyProperty()
        {
            // Act
            Container.RegisterType<ObjectWithFourFields>(
                Inject.Field(nameof(ObjectWithFourFields.ReadOnlyField), "test"));
        }
    }
}
