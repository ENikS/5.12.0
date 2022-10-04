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
#if !NET45
        [TestMethod]
        public void Injection_ByCountFirst()
        {
            // Arrange
            #region inject_count_first_arrange

            Container.RegisterType<SampleType>(new InjectionConstructor(new ResolvedParameter()));

            #endregion

            // Act
            #region inject_count_first_ctor_act

            var instance = Container.Resolve<SampleType>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }
#endif

        [TestMethod]
        public void Injection_ByCountFirstGeneric()
        {
            // Arrange
            #region inject_count_first_arrange_generic

            Container.RegisterType(typeof(SampleType<>), new InjectionConstructor(new GenericParameter("T")));

            #endregion

            // Act
            #region inject_count_first_ctor_act_generic

            var instance = Container.Resolve<SampleType<object>>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public virtual void Injection_ByCountNamedGeneric()
        {
            // Arrange
            Container.RegisterType<IService, Service>()
                     .RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            Container.RegisterType(typeof(SampleType<>),
                new InjectionConstructor(
                    new ResolvedParameter(typeof(IService)),
                    new GenericParameter("T")));

            var instance = Container.Resolve<SampleType<object>>();

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(Service) == instance.Service.GetType());
        }

        [TestMethod]
        public virtual void Injection_ByCountNameOverrideGeneric()
        {
            // Arrange
            Container.RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            #region inject_count_name_override_generic

            Container.RegisterType(typeof(SampleType<>),
                new InjectionConstructor(
                    new ResolvedParameter(typeof(IService),"two"),
                    new GenericParameter("T")));

            var instance = Container.Resolve<SampleType<object>>();

            // 2 == instance.Ctor
            // typeof(ServiceTwo) == instance.Service.GetType()

            #endregion

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(ServiceTwo) == instance.Service.GetType());
        }
    }
}
