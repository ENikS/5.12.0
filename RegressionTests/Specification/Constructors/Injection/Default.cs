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
        public void Injection_InjectDefaultCtor()
        {
            // Arrange
            #region inject_default_ctor_arrange

            Container.RegisterType<Service>(new InjectionConstructor());

            #endregion

            // Act
            #region inject_default_ctor_act

            var instance = Container.Resolve<Service>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void Injection_InjectDefaultCtorClosedGeneric()
        {
            // Arrange
            #region inject_default_ctor_closed_generic_arrange

            Container.RegisterType<Service<object>>(new InjectionConstructor());

            #endregion

            // Act
            #region inject_default_ctor_closed_generic_act

            var instance = Container.Resolve<Service<object>>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }


        [TestMethod]
        public void Injection_InjectDefaultCtorOpenGeneric()
        {
            // Arrange
            #region inject_default_ctor_open_generic_arrange

            Container.RegisterType(typeof(Service<>), new InjectionConstructor());

            #endregion

            // Act
            var instance = Container.Resolve<Service<object>>();

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }
    }
}
