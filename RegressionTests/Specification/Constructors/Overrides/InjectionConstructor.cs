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
    public partial class Constructors
    {
        [TestMethod]
        public virtual void Override_CtorParameter()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionConstructor(_data));

            // Act
            var value = Container.Resolve<Service>(new DependencyOverride<string>(_override));

            // Verify
            Assert.AreSame(_override, value.Data);
        }
    }

#if !NET45

    public partial class Constructors_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void Override_CtorParameter()
            => base.Override_CtorParameter();
    }

#endif
}
