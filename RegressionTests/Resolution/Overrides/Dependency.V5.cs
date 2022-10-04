using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace Resolution
{
    public partial class Overrides
    {
        [TestMethod]
        public void Type()
        {
            var nega = "none";

            // Arrange
            Container.RegisterType<IFoo, Foo2>()
                     .RegisterType<IFoo, Foo1>(Name);

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFoo>().Foo, typeof(Foo2));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFoo>(new DependencyOverride(typeof(IFoo), null, new Foo3())).Foo, typeof(Foo3));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFoo>(new DependencyOverride(typeof(IFoo), Name, new Foo3())).Foo, typeof(Foo2));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFoo>(new DependencyOverride(typeof(IFoo), nega, new Foo3())).Foo, typeof(Foo2));
        }

        [TestMethod]
        public void TypeNamed()
        {
            var nega = "none";

            // Arrange
            Container.RegisterType<IFoo, Foo2>()
                     .RegisterType<IFoo, Foo1>(Name);

            // Act / Validate
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFooName>().Foo, typeof(Foo1));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFooName>(new DependencyOverride(typeof(IFoo), null, new Foo3())).Foo, typeof(Foo1));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFooName>(new DependencyOverride(typeof(IFoo), Name, new Foo3())).Foo, typeof(Foo3));
            Assert.IsInstanceOfType(Container.Resolve<DependsOnIFooName>(new DependencyOverride(typeof(IFoo), nega, new Foo3())).Foo, typeof(Foo1));
        }

        [TestMethod]
        public void OptionalViaDependency()
        {
            // Setup
            IService x = new Service1();
            IService y = new Service2();

            // Act
            var result = Container.Resolve<Foo>(
                    Override.Dependency("Fred",   x),
                    Override.Dependency("George", y));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Fred);
            Assert.IsNull(result.George);
        }

        [TestMethod]
        public void OptionalViaDependencyAsType()
        {
            // Setup
            IService x = new Service1();
            IService y = new Service2();

            // Act
            var result = Container.Resolve<Foo>(
                    Override.Dependency(typeof(IService), "Fred", x),
                    Override.Dependency(typeof(IService), "George", y));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Fred);
            Assert.IsNotNull(result.George);
        }

        [TestMethod]
        public void OptionalViaDependencyAsGenericType()
        {
            // Setup
            IService x = new Service1();
            IService y = new Service2();

            // Act
            var result = Container.Resolve<Foo>(
                    Override.Dependency<IService>("Fred", x),
                    Override.Dependency<IService>("George", y));

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Fred);
            Assert.IsNotNull(result.George);
        }

        [TestMethod]
        public void ParameterOverrideCanResolveOverride()
        {
            // Setup
            Container.RegisterType<IService, Service1>()
                     .RegisterType<IService, Service2>("other");

            // Act
            var result = Container.Resolve<ObjectTakingASomething>(
                Override.Parameter("something", Resolve.Dependency<IService>("other")));

            // Verify
            Assert.IsInstanceOfType(result.MySomething, typeof(Service2));
        }


        [TestMethod]
        public void CanOverrideDependencyWithExplicitInjectionParameterValue()
        {
            // Setup
            Container
                .RegisterType<Outer>(new InjectionConstructor(typeof(Inner), 10))
                .RegisterType<Inner>(new InjectionConstructor(20, "ignored"));

            // resolves overriding only the parameter for the Bar instance

            // Act
            var instance = Container.Resolve<Outer>(
                Override.Parameter<int>(new InjectionParameter(50)).OnType<Inner>());

            // Verify
            Assert.AreEqual(10, instance.LogLevel);
            Assert.AreEqual(50, instance.Inner.LogLevel);
        }

        [TestMethod]
        public void InjectedParameterWithDependencyOverride()
        {
            // Setup
            var noOverride = "default";
            var depOverride = "custom-via-override";

            Container.RegisterType<TestType>(new InjectionConstructor(noOverride));
            // Act
            var defaultValue = Container.Resolve<TestType>().ToString();
            var depValue     = Container.Resolve<TestType>(Override.Dependency<string>(depOverride)).ToString();
            var propValue    = Container.Resolve<TestType>(Override.Parameter<string>(depOverride)).ToString();

            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(depOverride, depValue);
            Assert.AreSame(depOverride, propValue);
        }

        [TestMethod]
        public void InjectedFieldWithFielDependencyOverride()
        {
            // Setup
            var noOverride = "default";
            var depOverride = "custom-via-override";
            Container.RegisterType<TestType>(new InjectionField(nameof(TestType.DependencyField), noOverride));

            // Act
            var defaultValue = Container.Resolve<TestType>().DependencyField;
            var dependValue  = Container.Resolve<TestType>(Override.Dependency<string>(depOverride)).DependencyField;
            var fieldValue   = Container.Resolve<TestType>(Override.Field(nameof(TestType.DependencyField), depOverride)).DependencyField;

            // Verify
            Assert.AreSame(noOverride, defaultValue);
            Assert.AreSame(depOverride, dependValue);
            Assert.AreSame(depOverride, fieldValue);
        }
    }
}
