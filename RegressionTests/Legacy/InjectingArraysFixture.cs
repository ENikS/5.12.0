using Breaking.Changes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif


namespace Unity.V4
{
    [TestClass]
    public class InjectingArraysFixture
    {
        [TestMethod]
        public void CanConfigureContainerToCallConstructorWithArrayParameter()
        {
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            IUnityContainer container
                = new UnityContainer()
                .RegisterType<TypeWithArrayConstructorParameter>(
                    new InjectionConstructor(typeof(ILogger[])))
                .RegisterInstance<ILogger>("o1", o1)
                .RegisterInstance<ILogger>("o2", o2);

            TypeWithArrayConstructorParameter resolved = container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.IsTrue(resolved.Loggers.Contains(o1));
            Assert.IsTrue(resolved.Loggers.Contains(o2));
        }

        [TestMethod]
        public void CanConfigureContainerToCallConstructorWithArrayParameterWithNonGenericVersion()
        {
            ILogger o1 = new MockLogger();
            ILogger o2 = new SpecialLogger();

            IUnityContainer container = new UnityContainer()
                .RegisterType<TypeWithArrayConstructorParameter>(new InjectionConstructor(typeof(ILogger[])))
                .RegisterInstance<ILogger>("o1", o1)
                .RegisterInstance<ILogger>("o2", o2);

            TypeWithArrayConstructorParameter resolved = container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.IsNotNull(resolved.Loggers);
            Assert.AreEqual(2, resolved.Loggers.Length);
            Assert.IsTrue(resolved.Loggers.Contains(o1));
            Assert.IsTrue(resolved.Loggers.Contains(o2));
        }

        [TestMethod]
        public void CanConfigureContainerToInjectSpecificValuesIntoAnArray()
        {
            ILogger logger2 = new SpecialLogger();

            IUnityContainer container = new UnityContainer()
                .RegisterType<TypeWithArrayConstructorParameter>(
                new InjectionConstructor(
                    new ResolvedArrayParameter<ILogger>(
                        new ResolvedParameter<ILogger>("log1"),
                        typeof(ILogger),
                        logger2)))
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("log1");

            TypeWithArrayConstructorParameter result = container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.AreEqual(3, result.Loggers.Length);
            Assert.IsInstanceOfType(result.Loggers[0], typeof(SpecialLogger));
            Assert.IsInstanceOfType(result.Loggers[1], typeof(MockLogger));
            Assert.AreSame(logger2, result.Loggers[2]);
        }

        [TestMethod]
        public void CanConfigureContainerToInjectSpecificValuesIntoAnArrayWithNonGenericVersion()
        {
            ILogger logger2 = new SpecialLogger();

            IUnityContainer container = new UnityContainer()
                .RegisterType<TypeWithArrayConstructorParameter>(
                new InjectionConstructor(
                    new ResolvedArrayParameter(
                        typeof(ILogger),
                        new ResolvedParameter<ILogger>("log1"),
                        typeof(ILogger),
                        logger2)))
                .RegisterType<ILogger, MockLogger>()
                .RegisterType<ILogger, SpecialLogger>("log1");

            TypeWithArrayConstructorParameter result = container.Resolve<TypeWithArrayConstructorParameter>();

            Assert.AreEqual(3, result.Loggers.Length);
            Assert.IsInstanceOfType(result.Loggers[0], typeof(SpecialLogger));
            Assert.IsInstanceOfType(result.Loggers[1], typeof(MockLogger));
            Assert.AreSame(logger2, result.Loggers[2]);
        }

        [Ignore("Breaking Change V6, no longer validates during registration")]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreatingResolvedArrayParameterWithValuesOfNonCompatibleType()
        {
            ILogger logger2 = new SpecialLogger();

            new ResolvedArrayParameter<ILogger>(
                new ResolvedParameter<ILogger>("log1"),
                typeof(int),
                logger2);
        }

        [TestMethod]
        public void ResolvesArray()
        {
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            IUnityContainer container = new UnityContainer()
                .RegisterInstance("one", expected[0])
                .RegisterInstance("two", expected[1]);

            var result = container.Resolve<ILogger[]>();

            Assert.AreEqual(2, result.Length);
        }


        [TestMethod]
        public void ResolvesEnumerable()
        {
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            IUnityContainer container = new UnityContainer()
                .RegisterInstance(expected[0])
                .RegisterInstance(expected[1])
                .RegisterInstance("one", expected[0])
                .RegisterInstance("two", expected[1]);

            var result = container.Resolve<IEnumerable<ILogger>>()
                                  .ToArray();
// TODO: Document differences
#if NET45 || NET46 
            Assert.AreEqual(2, result.Length);
#else
            Assert.AreEqual(4, result.Length);
#endif
        }

        [TestMethod]
        public void ResolvesArrayTwice()
        {
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            IUnityContainer container = new UnityContainer()
                .RegisterInstance("one", expected[0])
                .RegisterInstance("two", expected[1]);

            var result = container.Resolve<ILogger[]>();
            var result1 = container.Resolve<ILogger[]>();

            Assert.AreNotSame(result, result1);
            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void ContainerAutomaticallyResolvesAllWhenInjectingArrays()
        {
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            IUnityContainer container = new UnityContainer()
                .RegisterInstance("one", expected[0])
                .RegisterInstance("two", expected[1]);

            TypeWithArrayConstructorParameter result = container.Resolve<TypeWithArrayConstructorParameter>();

            CollectionAssert.AreEqual(expected, result.Loggers);
        }

        [TestMethod]
        public void ContainerAutomaticallyResolvesAllWhenInjectingGenericArrays()
        {
            ILogger[] expected = new ILogger[] { new MockLogger(), new SpecialLogger() };
            IUnityContainer container = new UnityContainer()
                .RegisterInstance("one", expected[0])
                .RegisterInstance("two", expected[1])
                .RegisterType(typeof(GenericTypeWithArrayProperty<>),
                    new InjectionProperty("Prop"));

            var result = container.Resolve<GenericTypeWithArrayProperty<ILogger>>();
            CollectionAssert.AreEquivalent(result.Prop, new[] { expected[0], expected[1] });
        }

        public class TypeWithArrayConstructorParameter
        {
            public readonly ILogger[] Loggers;

            public TypeWithArrayConstructorParameter(ILogger[] loggers)
            {
                this.Loggers = loggers;
            }
        }

        public class GenericTypeWithArrayProperty<T>
        {
            public T[] Prop { get; set; }
        }

#region Test Data

        public interface ILogger
        {
        }

        public class MockLogger : ILogger
        {
        }

        public class SpecialLogger : ILogger
        {
        }

#endregion
    }
}
