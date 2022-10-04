using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Resolution
{
    public partial class Arrays
    {
        [TestMethod]
        public void CanConfigureContainerToCallConstructorWithArrayParameter()
        {
            // Arrange
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterType<TypeWithArrayConstructorParameter>( new InjectionConstructor(typeof(ILogger[])))
                     .RegisterInstance<ILogger>("o1", o1)
                     .RegisterInstance<ILogger>("o2", o2);

            // Act
            var resolved = Container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.AreSame(o1, resolved.Loggers[0]);
            Assert.AreSame(o2, resolved.Loggers[1]);
        }

        [TestMethod]
        public void CanConfigureContainerToCallConstructorWithArrayParameterWithNonGenericVersion()
        {
            // Arrange
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            Container.RegisterType<TypeWithArrayConstructorParameter>(new InjectionConstructor(typeof(ILogger[])))
                     .RegisterInstance<ILogger>("o1", o1)
                     .RegisterInstance<ILogger>("o2", o2);

            // Act
            var resolved = Container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.AreSame(o1, resolved.Loggers[0]);
            Assert.AreSame(o2, resolved.Loggers[1]);
        }

        [TestMethod]
        public void CanConfigureContainerToInjectSpecificValuesIntoAnArray()
        {
            // Arrange
            ILogger logger2 = new SpecialLogger();

            Container.RegisterType<TypeWithArrayConstructorParameter>(
                        new InjectionConstructor(
                            new ResolvedArrayParameter<ILogger>(
                                new ResolvedParameter<ILogger>("log1"),
                                typeof(ILogger),
                                logger2)))
                     .RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, SpecialLogger>("log1");

            // Act
            var result = Container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.AreEqual(3, result.Loggers.Length);
            Assert.IsInstanceOfType(result.Loggers[0], typeof(SpecialLogger));
            Assert.IsInstanceOfType(result.Loggers[1], typeof(MockLogger));
            Assert.AreSame(logger2, result.Loggers[2]);
        }

        [TestMethod]
        public void CanConfigureContainerToInjectSpecificValuesIntoAnArrayWithNonGenericVersion()
        {
            // Arrange
            ILogger logger2 = new SpecialLogger();

            Container.RegisterType<TypeWithArrayConstructorParameter>(
                        new InjectionConstructor(
                            new ResolvedArrayParameter(
                                typeof(ILogger),
                                new ResolvedParameter<ILogger>("log1"),
                                typeof(ILogger),
                                logger2)))
                     .RegisterType<ILogger, MockLogger>()
                     .RegisterType<ILogger, SpecialLogger>("log1");

            // Act
            var result = Container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.AreEqual(3, result.Loggers.Length);
            Assert.IsInstanceOfType(result.Loggers[0], typeof(SpecialLogger));
            Assert.IsInstanceOfType(result.Loggers[1], typeof(MockLogger));
            Assert.AreSame(logger2, result.Loggers[2]);
        }

        [TestMethod]
        public void ContainerAutomaticallyResolvesAllWhenInjectingArrays()
        {
            // Arrange
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            Container.RegisterInstance<ILogger>("one", expected[0])
                     .RegisterInstance<ILogger>("two", expected[1]);

            var result = Container.Resolve<TypeWithArrayConstructorParameter>();


            // Validate
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Loggers.Length);
            Assert.AreSame(expected[0], result.Loggers[0]);
            Assert.AreSame(expected[1], result.Loggers[1]);
        }
    }
}
