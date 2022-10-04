using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public partial class Properties
    {
        [TestMethod]
        public void Injection_GenericPropertyIsActuallyInjected()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                        new InjectionConstructor(),
                    Inject.Property("Inner", Resolve.Parameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");
            
            // Act
            ICommand<Account> result = Container.Resolve<ICommand<Account>>();

            // Verify
            LoggingCommand<Account> actualResult = (LoggingCommand<Account>)result;
            Assert.IsNotNull(actualResult.Inner);
            Assert.IsInstanceOfType(actualResult.Inner, typeof(ConcreteCommand<Account>));
        }

        [TestMethod]
        public void Injection_CanInjectNestedGenerics()
        {
            // Setup
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                         new InjectionConstructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                     .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            var cmd = Container.Resolve<ICommand<Customer?>>();
            var logCmd = (LoggingCommand<Customer?>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<Customer?>));
        }


        [TestMethod]
        public void Injection_CanChainGenericTypes()
        {
            // Setup
            Container.RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>), 
                        new InjectionConstructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                     .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            var md = Container.Resolve<ICommand<User>>("concrete");
            ICommand<User> cmd = Container.Resolve<ICommand<User>>();
            LoggingCommand<User> logCmd = (LoggingCommand<User>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<User>));
        }

        [TestMethod]
        public void Injection_CanChainGenericTypesViaRegisterTypeMethod()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>), 
                    new InjectionConstructor(Resolve.Parameter(typeof(ICommand<>), "concrete")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete");

            // Act
            ICommand<User> cmd = Container.Resolve<ICommand<User>>();
            LoggingCommand<User> logCmd = (LoggingCommand<User>)cmd;

            // Verify
            Assert.IsNotNull(logCmd.Inner);
            Assert.IsInstanceOfType(logCmd.Inner, typeof(ConcreteCommand<User>));
        }
    }
}
