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
        [Ignore("v6 Does not validate on register")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoConstructor()
        {
            // Act
            Container.RegisterType<TypeWithAmbiguousCtors>(
                new InjectionConstructor(new ResolvedParameter(typeof(object))));
        }

        [TestMethod]
        public virtual void Injection_MultipleConstructor()
        {
            // Arrange
            Container.RegisterType<TypeWithAmbiguousCtors>(
                new InjectionConstructor(),
                new InjectionConstructor());

            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
        }

    }

#if !NET45
    public partial class Constructors_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_MultipleConstructor() 
            => base.Injection_MultipleConstructor();
    }
#endif
}
