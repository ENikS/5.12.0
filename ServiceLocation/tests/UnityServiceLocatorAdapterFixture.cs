using CommonServiceLocator;
using Microsoft.Practices.Unity.ServiceLocation.Tests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.ServiceLocation.Tests
{
    /// <summary>
    /// Summary description for UnityServiceLocatorAdapterFixture
    /// </summary>
    [TestClass]
    public class UnityServiceLocatorAdapterFixture : ServiceLocatorFixture
    {
        protected override IServiceLocator CreateServiceLocator()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType<ILogger, AdvancedLogger>()
                .RegisterType<ILogger, SimpleLogger>(typeof(SimpleLogger).FullName)
                .RegisterType<ILogger, AdvancedLogger>(typeof(AdvancedLogger).FullName);

            return new UnityServiceLocator(container);
        }

        [TestInitialize]
        public void Setup()
        {
            this.locator = this.CreateServiceLocator();
        }

        [TestMethod]
        public new void GetInstance()
        {
            base.GetInstance();
        }

        [TestMethod]
        public new void AskingForInvalidComponentShouldRaiseActivationException()
        {
            base.AskingForInvalidComponentShouldRaiseActivationException();
        }

        [TestMethod]
        public new void GetNamedInstance()
        {
            base.GetNamedInstance();
        }

        [TestMethod]
        public new void GetNamedInstance2()
        {
            base.GetNamedInstance2();
        }

        [TestMethod]
        public new void GetUnknownInstance2()
        {
            base.GetUnknownInstance2();
        }

        [TestMethod]
        public new void GetAllInstances()
        {
            base.GetAllInstances();
        }

        [TestMethod]
        public new void GetAllInstance_ForUnknownType_ReturnEmptyEnumerable()
        {
            base.GetAllInstance_ForUnknownType_ReturnEmptyEnumerable();
        }

        [TestMethod]
        public new void GenericOverload_GetInstance()
        {
            base.GenericOverload_GetInstance();
        }

        [TestMethod]
        public new void GenericOverload_GetInstance_WithName()
        {
            base.GenericOverload_GetInstance_WithName();
        }

        [TestMethod]
        public new void Overload_GetInstance_NoName_And_NullName()
        {
            base.Overload_GetInstance_NoName_And_NullName();
        }

        [TestMethod]
        public new void GenericOverload_GetAllInstances()
        {
            base.GenericOverload_GetAllInstances();
        }

        [TestMethod]
        public void Get_WithZeroLenName_ReturnsDefaultInstance()
        {
            Assert.AreSame(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance<ILogger>(String.Empty).GetType());
        }
    }
}
