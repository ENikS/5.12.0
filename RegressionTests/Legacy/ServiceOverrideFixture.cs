using Microsoft.VisualStudio.TestTools.UnitTesting;
#if V4
using System;
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif


namespace Unity.V4
{
    [TestClass]
    public class ServiceOverrideFixture
    {
        [TestMethod]
        public void CanProvideConstructorParameterViaResolveCall()
        {
            const int ConfiguredValue = 15; // Just need a number, value has no significance.
            const int ExpectedValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer()
                .RegisterType<SimpleTestObject>(new InjectionConstructor(ConfiguredValue));

            var result =
                container.Resolve<SimpleTestObject>(new ParameterOverride("x", ExpectedValue));

            Assert.AreEqual(ExpectedValue, result.X);
        }

        [TestMethod]
        public void OverrideDoesntLastAfterResolveCall()
        {
            const int ConfiguredValue = 15; // Just need a number, value has no significance.
            const int OverrideValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer()
                .RegisterType<SimpleTestObject>(new InjectionConstructor(ConfiguredValue));

            container.Resolve<SimpleTestObject>(new ParameterOverride("x", OverrideValue).OnType<SimpleTestObject>());

            var result = container.Resolve<SimpleTestObject>();

            Assert.AreEqual(ConfiguredValue, result.X);
        }

        [TestMethod]
        public void OverrideIsUsedInRecursiveBuilds()
        {
            const int ExpectedValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer();

            var result = container.Resolve<ObjectThatDependsOnSimpleObject>(
                new ParameterOverride("x", ExpectedValue));

            Assert.AreEqual(ExpectedValue, result.TestObject.X);
        }

        [TestMethod]
        public void OverrideIsUsedInRecursiveBuilds_Registered()
        {
            const int ExpectedValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer();

            container.RegisterType<SimpleTestObject>()
                     .RegisterType<ObjectThatDependsOnSimpleObject>();

            var result = container.Resolve<ObjectThatDependsOnSimpleObject>(
                new ParameterOverride("x", ExpectedValue));

            Assert.AreEqual(ExpectedValue, result.TestObject.X);
        }

        [TestMethod]
        public void NonMatchingOverridesAreIgnored()
        {
            const int ExpectedValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer();

            var result = container.Resolve<SimpleTestObject>(
                new ParameterOverrides
                {
                    { "y", ExpectedValue * 2 },
                    { "x", ExpectedValue }
                }.OnType<SimpleTestObject>());

            Assert.AreEqual(ExpectedValue, result.X);
        }


        [TestMethod]
        public void NonMatchingOverridesAreIgnored_Registered()
        {
            const int ExpectedValue = 42; // Just need a number, value has no significance.
            var container = new UnityContainer().RegisterType<SimpleTestObject>();

            var result = container.Resolve<SimpleTestObject>(
                new ParameterOverrides
                {
                    { "y", ExpectedValue * 2 },
                    { "x", ExpectedValue }
                }.OnType<SimpleTestObject>());

            Assert.AreEqual(ExpectedValue, result.X);
        }

        [TestMethod]
        public void DependencyOverrideOccursEverywhereTypeMatches()
        {
            var container = new UnityContainer()
                .RegisterType<ObjectThatDependsOnSimpleObject>(new InjectionProperty("OtherTestObject"))
                .RegisterType<SimpleTestObject>(new InjectionConstructor());

            var overrideValue = new SimpleTestObject(15); // arbitrary value

            var result = container.Resolve<ObjectThatDependsOnSimpleObject>(
                new DependencyOverride<SimpleTestObject>(overrideValue));

            Assert.AreSame(overrideValue, result.TestObject);
            Assert.AreSame(overrideValue, result.OtherTestObject);
        }

        [TestMethod]
        public void ParameterOverrideCanResolveOverride()
        {
            var container = new UnityContainer()
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            var result = container.Resolve<ObjectTakingASomething>(
                new ParameterOverride("something", new ResolvedParameter<ISomething>("other")));

            Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
        }

        [TestMethod]
        public void ParameterOverrideCanResolveOverride_Registered()
        {
            var container = new UnityContainer()
                .RegisterType<ObjectTakingASomething>()
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            var result = container.Resolve<ObjectTakingASomething>(
                new ParameterOverride("something", new ResolvedParameter<ISomething>("other")));

            Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
        }

        [TestMethod]
        public void CanOverridePropertyValue()
        {
            var container = new UnityContainer()
                .RegisterType<ObjectTakingASomething>(
                    new InjectionConstructor(),
                    new InjectionProperty("MySomething"))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            var result = container.Resolve<ObjectTakingASomething>(
                new PropertyOverride("MySomething", new ResolvedParameter<ISomething>("other")).OnType<ObjectTakingASomething>());

            Assert.IsNotNull(result.MySomething);
            Assert.IsInstanceOfType(result.MySomething, typeof(Something2));
        }

        [TestMethod]
        public void PropertyValueOverrideForTypeDifferentThanResolvedTypeIsIgnored()
        {
            var container = new UnityContainer()
                .RegisterType<ObjectTakingASomething>(
                    new InjectionConstructor(),
                    new InjectionProperty("MySomething"))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            var result = container.Resolve<ObjectTakingASomething>(
                new PropertyOverride("MySomething", new ResolvedParameter<ISomething>("other")).OnType<ObjectThatDependsOnSimpleObject>());

            Assert.IsNotNull(result.MySomething);
            Assert.IsInstanceOfType(result.MySomething, typeof(Something1));
        }

        [TestMethod]
        public void CanOverridePropertyValueWithNullWithExplicitInjectionParameter()
        {
            var container = new UnityContainer()
                .RegisterType<ObjectTakingASomething>(
                    new InjectionConstructor(),
                    new InjectionProperty("MySomething"))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>("other");

            var result = container.Resolve<ObjectTakingASomething>(
                new PropertyOverride("MySomething", new InjectionParameter<ISomething>(null)).OnType<ObjectTakingASomething>());

            Assert.IsNull(result.MySomething);
        }
#if NET45
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingAPropertyOverrideForANullValueThrows()
        {
            new PropertyOverride("ignored", null);
        }
#endif
        [TestMethod]
        public void CanOverrideDependencyWithExplicitInjectionParameterValue()
        {
            var container = new UnityContainer()
                .RegisterType<Outer>(new InjectionConstructor(typeof(Inner), 10))
                .RegisterType<Inner>(new InjectionConstructor(20, "ignored"));

            // resolves overriding only the parameter for the Bar instance

            var instance = container.Resolve<Outer>(new DependencyOverride<int>(new InjectionParameter(50)).OnType<Inner>());

            Assert.AreEqual(10, instance.LogLevel);
            Assert.AreEqual(50, instance.Inner.LogLevel);
        }

#if NET45
#pragma warning disable CS0618 // Type or member is obsolete
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingATypeOverrideForANullTypeThrows()
        {
            new TypeBasedOverride(null, new PropertyOverride("ignored", 10));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingATypeOverrideForANullOverrideThrows()
        {
            new TypeBasedOverride(typeof(object), null);
        }
#pragma warning restore CS0618 // Type or member is obsolete
#endif
        public class SimpleTestObject
        {
            public SimpleTestObject(int x)
            {
                X = x;
            }

            public int X { get; private set; }
        }

        public class ObjectThatDependsOnSimpleObject
        {
            public SimpleTestObject TestObject { get; set; }

            public ObjectThatDependsOnSimpleObject(SimpleTestObject testObject)
            {
                TestObject = testObject;
            }

            public SimpleTestObject OtherTestObject { get; set; }
        }

        public interface ISomething { }
        public class Something1 : ISomething { }
        public class Something2 : ISomething { }

        public class ObjectTakingASomething
        {
            public ISomething MySomething { get; set; }
            
            public ObjectTakingASomething() { }

            [InjectionConstructor]
            public ObjectTakingASomething(ISomething something)
            {
                MySomething = something;
            }
        }

        public class Outer
        {
            public Outer(Inner inner, int logLevel)
            {
                this.Inner = inner;
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
            public Inner Inner { get; private set; }
        }

        public class Inner
        {
            public Inner(int logLevel, string label)
            {
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
        }
    }
}
