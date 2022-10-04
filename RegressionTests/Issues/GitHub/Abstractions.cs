using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Issues
{
    public partial class GitHub
    {
        [TestMethod]
        // https://github.com/unitycontainer/abstractions/issues/96
        public virtual void Issue_Abstractions_96()
        {
            // Act
            var ctor = new InjectionConstructor();
            Container.RegisterType<IService, Service>(ctor);
            Container.RegisterType<IService, Service>("name", ctor);
        }

#if !NET45
        [TestMethod]
        // https://github.com/unitycontainer/abstractions/issues/83
        public void Issue_Abstractions_83()
        {
            // Arrange
            Container.RegisterInstance(Name);
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property), Name));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Container);
            Assert.IsNotNull(result.Property);
        }
#endif
    }

    public partial class GitHub_Diagnostic
    {
#if !NET45
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        // https://github.com/unitycontainer/abstractions/issues/96
        public override void Issue_Abstractions_96()
        {
            // Act
            var ctor = new InjectionConstructor();
            Container.RegisterType<IService, Service>(ctor);
            Container.RegisterType<IService, Service>("name", ctor);
        }
#endif
    }
}
